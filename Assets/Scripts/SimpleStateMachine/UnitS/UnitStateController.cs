using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStateController : ASimpleStateController
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private NavMeshObstacle navObstacle;

    private BattleUnit _currentUnit;
    private Transform _chaseTarget;

    public override UnitStateController UnitController => this;
    public NavMeshAgent NavAgent => navAgent;
    public NavMeshObstacle NavObstacle => navObstacle;
    public BattleUnit CurrentUnit => _currentUnit;
    public Transform ChaseTarget => _chaseTarget;
    public bool IngoreUnitAndTower { get; set; } = false;


    public void Init(BattleUnit unit)
    {
        _currentUnit = unit;
        _chaseTarget = _currentUnit.TowerTarget;
        navAgent.speed = _currentUnit.MoveSpeed;

        navAgent.enabled = false;
    }

    public void FinishInit(Transform finishTarget, float speed)
    {
        _chaseTarget = finishTarget;
        navAgent.speed = speed;
        IngoreUnitAndTower = true;
    }

}
