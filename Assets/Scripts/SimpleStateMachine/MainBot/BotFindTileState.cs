﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFindTileState : AState
{
    [SerializeField, Range(1, 101)] private int minTilePercentage; 
    [SerializeField] private float checkRadius;
    [SerializeField] private float timeToGrabTile;
    [SerializeField] private LayerMask whatIsTile;


    private int _randomTilesToGrab;
    private int _minTileToGrab;
    private int _maxTileToGrab;

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
        SetMinMaxTiles();
    }

    private void SetMinMaxTiles()
    {
        _maxTileToGrab = GameController.Data.MaxTiles;
        _minTileToGrab = Mathf.RoundToInt((_maxTileToGrab * minTilePercentage) / 100);
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        LocalInit();
        ChooseTilesToCollect();
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
        _randomTilesToGrab = Random.Range(_minTileToGrab, _maxTileToGrab);
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
