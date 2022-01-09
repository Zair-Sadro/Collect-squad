using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStateController : ASimpleStateController
{
    [SerializeField] private NavMeshAgent navAgent;

    private BattleUnit _currentUnit;
    private Transform _chaseTarget;

    public override UnitStateController UnitController => this;
    public NavMeshAgent NavAgent => navAgent;
    public BattleUnit CurrentUnit => _currentUnit;
    public Transform ChaseTarget => _chaseTarget;



    public void Init(BattleUnit unit)
    {
        _currentUnit = unit;
        _chaseTarget = _currentUnit.TowerTarget;
        navAgent.speed = _currentUnit.MoveSpeed;
    }

}
