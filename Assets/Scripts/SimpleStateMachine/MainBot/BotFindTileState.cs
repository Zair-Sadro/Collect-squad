﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFindTileState : AState
{
    [SerializeField] private int minTileToGrab;
    [SerializeField] private int maxTileToGrab;
    [SerializeField] private float checkRadius;
    [SerializeField] private float timeToGrabTile;
    [SerializeField] private LayerMask whatIsTile;


    private int _randomTilesToGrab;
    private float _currentGrabTime;

    private BotStateController _botController;
    private Animator _animator;
    private TileSetter _tileSetter;
    private NavMeshAgent _navAgent;
    private List<ASpawnedObject> _allActiveTiles;
    private List<Tile> _choosedTiles = new List<Tile>();

    public override StateType StateType => StateType.BotFindTile;

    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
        _botController = _stateController.BotController;
    }

    private void LocalInit()
    {
        _navAgent = _botController.NavAgent;
        _tileSetter = _botController.BotTileSetter;
        _animator = _botController.Animator;
        _navAgent.speed = _botController.MoveSpeed;
        _allActiveTiles = _botController.TileSpawner.ObjectPooler.GetPool();
    }


    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        LocalInit();
        ChooseTilesToCollect();
        _currentGrabTime = maxTileToGrab;
       // StartCoroutine(TakingTiles(timeToGrabTile));
    }


    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        CheckForTile();
    }


    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
        _choosedTiles.Clear();
        _animator.SetBool("Run", false);
    }

    private IEnumerator TakingTiles(float time)
    {
        var target = DesiredTile();

        while(_tileSetter.Tiles.Count < _randomTilesToGrab)
        {
            
            if(_currentGrabTime <= 0)
            {
                _animator.SetBool("Run", false);
                if(_navAgent.enabled)
                    _navAgent.ResetPath();

                target = DesiredTile();
                _currentGrabTime = maxTileToGrab;
            }
            else
            {
                _currentGrabTime -= Time.deltaTime;
                _animator.SetBool("Run", true);
                SetTarget(target);
            }

            yield return null;
        }
        
        _stateController.ChangeState(StateType.BotTowerChoose);
    }

    private Vector3 DesiredTile()
    {
        Vector3 path = new Vector3(0, 0, -25);

        for (int i = 0; i < _choosedTiles.Count; i++)
        {
            if (!_choosedTiles[i].IsTaken && _choosedTiles[i].gameObject.activeInHierarchy)
                path = _choosedTiles[i].transform.localPosition;
        }
        return path;
    }

    private void SetTarget(Vector3 target)
    {
        if (_navAgent.enabled)
            _navAgent.destination = target;
    }

    private void ChooseTilesToCollect()
    {
        for (int i = 0; i < GetRandomTileAmountToGrab(); i++)
            _choosedTiles.Add((Tile)_allActiveTiles[i]);
    }
    private int GetRandomTileAmountToGrab()
    {
        _randomTilesToGrab = Random.Range(minTileToGrab, maxTileToGrab);
        return _randomTilesToGrab;
    }

    private void CheckForTile()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, checkRadius, whatIsTile);

        if(colls.Length > 0)
        {
            _animator.SetBool("Run", true);

            for (int i = 0; i < colls.Length; i++)
            {
                if(colls[i].TryGetComponent(out Tile tile))
                {
                    if(_navAgent.enabled && !_navAgent.hasPath)
                        SetTarget(tile.transform.localPosition);
                }
            }
        }

        if(_tileSetter.Tiles.Count >= _randomTilesToGrab)
            _stateController.ChangeState(StateType.BotTowerChoose);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
