using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFindTileState : AState
{
    [SerializeField] private int minTileToGrab;
    [SerializeField] private int maxTileToGrab;


    private BotStateController _botController;
    private int _randomTilesToGrab;
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
        StartCoroutine(TakingTiles());
    }


    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;
    }


    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
        _choosedTiles.Clear();
        _animator.SetBool("Run", false);
    }

    private IEnumerator TakingTiles()
    {
        while(_tileSetter.Tiles.Count != _randomTilesToGrab)
        {
            _animator.SetBool("Run", true);

            for (int i = 0; i < _choosedTiles.Count; i++)
            {
                if (!_choosedTiles[i].IsTaken && _choosedTiles[i].gameObject.activeInHierarchy)
                {
                    if(!_navAgent.hasPath)
                        SetTarget(_choosedTiles[i].transform.localPosition);
                }
            }
            yield return null;
        }
        _stateController.ChangeState(StateType.BotTowerChoose);
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
}
