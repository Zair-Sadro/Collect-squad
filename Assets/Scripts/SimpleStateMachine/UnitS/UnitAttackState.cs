using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class UnitAttackState : AState
{
    [SerializeField] private float timeToCheckTargets = 1;
    [SerializeField] private bool isMovingWhenAttack;
    [SerializeField] private AWeapon weapon;
    [SerializeField] private Animator unitAnimator;


    private NavMeshAgent _navAgent;
    private NavMeshObstacle _navObstacle;
    private BattleUnit _currentUnit;

    private float _curCheckTime;

    public Transform AttackingTarget { get; set; }
    public override StateType StateType => StateType.UnitAttack;

    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        LocalInit();
        unitAnimator.SetBool("Run", false);
    }

    private void LocalInit()
    {
        _navAgent = _stateController.UnitController.NavAgent;
        _navObstacle = _stateController.UnitController.NavObstacle;
        _currentUnit = _stateController.UnitController.CurrentUnit;

        _navAgent.enabled = isMovingWhenAttack;
        _navObstacle.enabled = isMovingWhenAttack;

        _curCheckTime = timeToCheckTargets;
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        if (AttackingTarget != null)
        {
            weapon.Attack();

            if (_curCheckTime < 0)
                _stateController.ChangeState(StateType.UnitChase);
            else
                _curCheckTime -= Time.deltaTime;

            _currentUnit.transform.LookAt(AttackingTarget);
        }
        else
            _stateController.ChangeState(StateType.UnitChase);

    }

    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
    }

    public void SetTarget(Transform target)
    {
        AttackingTarget = target;
    }
}
