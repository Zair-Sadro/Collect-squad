using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : AState
{
    [SerializeField] private float timeToCheckTargets = 1;
    [SerializeField] private bool isMovingWhenAttack;
    [SerializeField] private AWeapon weapon;
    [SerializeField] private Animator unitAnimator;

    private NavMeshAgent _navAgent;
    private BattleUnit _currentUnit;

    private float _curCheckTime;

    public Transform AttackingTarget { get; set; }
    public override StateType StateType => StateType.Attack;

    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        LocalInit();
    }

    private void LocalInit()
    {
        _navAgent = _stateController.UnitController.NavAgent;
        _currentUnit = _stateController.UnitController.CurrentUnit;
        _navAgent.enabled = isMovingWhenAttack;
        _curCheckTime = timeToCheckTargets;
        unitAnimator.SetTrigger("Attack");
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        if (AttackingTarget != null)
        {
            weapon.Attack();

            if (_curCheckTime < 0)
                _stateController.ChangeState(StateType.Chase);
            else
                _curCheckTime -= Time.deltaTime;

            _currentUnit.transform.LookAt(AttackingTarget);
        }
        else
            _stateController.ChangeState(StateType.Chase);

    }

    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
    }
}
