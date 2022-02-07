﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BotTowerChooseState : AState
{
    [SerializeField, Range(0, 101)] private int chanceToSwapTower;
    [SerializeField, Range(0, 101)] private int chanceToBuildStrongerTower;
    [SerializeField, Min(0)] private float timeInBuildZone;
    [SerializeField] private List<Transform> botTowers = new List<Transform>();

    private BotStateController _botStateControls;
    private TileSetter _tileSetter;
    private Animator _animator;
    private NavMeshAgent _navAgent;
    private List<TowerBuildPlatform> _playerBuildPlatforms;

    private Vector3 _target;

    public override StateType StateType => StateType.BotTowerChoose;


    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
        _botStateControls = _stateController.BotController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        LocalInit();
        _target = TowerToGo();
        StartCoroutine(WalkToTower());
    }

    private void LocalInit()
    {
        _playerBuildPlatforms = _botStateControls.PlayerPlatforms;
        _tileSetter = _botStateControls.BotTileSetter;
        _navAgent = _botStateControls.NavAgent;
        _navAgent.speed = _botStateControls.MoveSpeed;
        _animator = _botStateControls.Animator;
    }


    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;
    }


    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;

        if(_navAgent.enabled)
            _navAgent.ResetPath();
    }

    private void SetTarget(Vector3 target)
    {
        if (_navAgent.enabled)
            _navAgent.destination = target;
    }

    private IEnumerator WalkToTower()
    {
        while(Vector3.Distance(transform.parent.position, _target) > 3)
        {
            _animator.SetBool("Run", true);
            SetTarget(_target);
            yield return null;
        }
        StartCoroutine(WaitForTime(timeInBuildZone));
    }

    private IEnumerator WaitForTime(float time)
    {
        _animator.SetBool("Run", false);
       
        if(_botStateControls.TowerToBuild != null)
            CheckTower(_botStateControls.TowerToBuild);

        if (_botStateControls.TowerToBuild != null)
            TryBuildTower(_botStateControls.TowerToBuild, _botStateControls.TowerToBuild.ActiveTower);

        yield return new WaitForSeconds(time);
        _stateController.ChangeState(StateType.BotFindTile);
    }

    private void CheckTower(TowerBuildPlatform towerToBuild)
    {
        var randomChance = Random.Range(0, 101);
        if(randomChance <= chanceToSwapTower)
        {
            if(towerToBuild != null && towerToBuild.ActiveTower.CurrentLevel.LevelType > 0)
            {
                if(towerToBuild.ActiveTower.Data.Type != GetStrongerTower(towerToBuild.OppositeTower.ActiveTower.Data.Type))
                    TrySwapTower(towerToBuild);
            }
        }
    }

    private Vector3 TowerToGo()
    {
        Vector3 target = Vector3.zero;

        for (int i = 0; i < _playerBuildPlatforms.Count; i++)
        {

            if (_playerBuildPlatforms[i].ActiveTower.CurrentLevel.LevelType > 0 &&
                _playerBuildPlatforms[i].OppositeTower.ActiveTower.CurrentLevel.LevelType == TowerLevelType.None)
            {
                var desiredTower = _playerBuildPlatforms[i].OppositeTower;

                if (_navAgent.enabled)
                    _navAgent.ResetPath();

                target = desiredTower.transform.position;
                _tileSetter.SetDiseredBuild(desiredTower);

                return target;
            }

            if (_navAgent.enabled)
                _navAgent.ResetPath();

            var randomTower = _playerBuildPlatforms[Random.Range(0, _playerBuildPlatforms.Count)].OppositeTower;
            target = randomTower.transform.position;
            _tileSetter.SetDiseredBuild(randomTower);

        }
        return target;
    }

    private void TryBuildTower(TowerBuildPlatform towerToBuild, ATowerObject activeTower)
    {
        if (activeTower.CurrentLevel.LevelType > 0 && activeTower != null || !_botStateControls.InBuildZone && towerToBuild == null)
            return;

        if (towerToBuild.TilesToUpgrade == 0 && activeTower.CurrentLevel.LevelType == 0)
        {
            bool isOppositeTowerBuild = towerToBuild.OppositeTower.ActiveTower.CurrentLevel.LevelType > 0;
            if (isOppositeTowerBuild)
                TryBuildStrongerTower(towerToBuild);
            else
            {
                var randTower = towerToBuild.Towers.Where(t => t.CurrentLevel.LevelType == TowerLevelType.level1).ToList();
                towerToBuild.BuiltTower(randTower[Random.Range(0, randTower.Count)]);
                towerToBuild.IsTowerBuild = true;
            }
        }
    }


    private void TryBuildStrongerTower(TowerBuildPlatform towerToBuild)
    {
        var randomChance = Random.Range(0, 101);
        if(randomChance <= chanceToBuildStrongerTower)
            towerToBuild.BuiltTower(GetStrongerTower(towerToBuild.OppositeTower.ActiveTower.Data.Type));
    }

    private void TrySwapTower(TowerBuildPlatform towerToBuild)
    {
        var randomChance = Random.Range(0, 101);
        if (randomChance <= chanceToBuildStrongerTower)
        {
            Debug.Log("<color=red> Bot swaped tower </color>");
            towerToBuild.BuiltTower(GetSwapTower(GetStrongerTower(towerToBuild.OppositeTower.ActiveTower.Data.Type)));
        }
           
    }

    private TowerObject GetSwapTower(UnitType type)
    {
        if (_botStateControls.TowerToBuild && _botStateControls.TowerToBuild.ActiveTower.CurrentLevel.LevelType > 0)
            for (int i = 0; i < _botStateControls.TowerToBuild.Towers.Count; i++)
            {
                if (_botStateControls.TowerToBuild.Towers[i].Data.Type == type &&
                   _botStateControls.TowerToBuild.Towers[i].CurrentLevel.LevelType == _botStateControls.TowerToBuild.ActiveTower.CurrentLevel.LevelType)
                    return (TowerObject)_botStateControls.TowerToBuild.Towers[i];
            }


        return null;
    }

    private UnitType GetStrongerTower(UnitType type)
    {
        UnitType strongerType = UnitType.None;

        switch (type)
        {
            case UnitType.Sword:
                strongerType = UnitType.Spear;

                break;
            case UnitType.Spear:
                strongerType = UnitType.Bow;

                break;
            case UnitType.Bow:
                strongerType = UnitType.Sword;
                break;
        }

        return strongerType;
    }
}
