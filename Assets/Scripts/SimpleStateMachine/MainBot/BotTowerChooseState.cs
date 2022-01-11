using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BotTowerChooseState : AState
{
    [SerializeField, Min(0)] private float timeInBuildZone;
    [SerializeField] private float runToTowerTime;
    [SerializeField] private List<Transform> botTowers = new List<Transform>();
    [SerializeField] private float distance;

    private TileSetter _tileSetter;
    private Animator _animator;
    private NavMeshAgent _navAgent;
    private List<TowerBuildPlatform> _playerBuildPlatforms;

    private Vector3 _target;

    public override StateType StateType => StateType.BotTowerChoose;


    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        LocalInit();
        _target = TowerToGo();
        StartCoroutine(WalkToTower(runToTowerTime));
    }

    private void LocalInit()
    {
        _playerBuildPlatforms = _stateController.BotController.PlayerPlatforms;
        _tileSetter = _stateController.BotController.BotTileSetter;
        _navAgent = _stateController.BotController.NavAgent;
        _navAgent.speed = _stateController.BotController.MoveSpeed;
        _animator = _stateController.BotController.Animator;
    }


    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;
    }


    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
        _navAgent.ResetPath();
    }

    private void SetTarget(Vector3 target)
    {
        if (_navAgent.enabled)
            _navAgent.destination = target;
    }

    private IEnumerator WalkToTower(float time)
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
        yield return new WaitForSeconds(time);
        _stateController.ChangeState(StateType.BotFindTile);
    }

    private Vector3 TowerToGo()
    {
        Vector3 target = Vector3.zero;

        for (int i = 0; i < _playerBuildPlatforms.Count; i++)
        {

            if (_playerBuildPlatforms[i].ActiveTower.CurrentLevel.LevelType > 0)
            {
                if (_navAgent.enabled)
                    _navAgent.ResetPath();

                target = _playerBuildPlatforms[i].OppositeTower.transform.position;
                _tileSetter.SetDiseredBuild(_playerBuildPlatforms[i].OppositeTower);
            }
            else
            {
                var randomTower = _playerBuildPlatforms[Random.Range(0, _playerBuildPlatforms.Count)].OppositeTower;
                target = randomTower.transform.position;
                _tileSetter.SetDiseredBuild(randomTower);
            }
        }
        return target;
    }
}
