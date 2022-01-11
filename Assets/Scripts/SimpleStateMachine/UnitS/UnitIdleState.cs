using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitIdleState : AState
{
    [SerializeField] private Animator animator;

    private NavMeshAgent _navAgent;

    public override StateType StateType => StateType.Idle;

    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        _navAgent = _stateController.UnitController.NavAgent;

        if(_navAgent.enabled)
            _navAgent.ResetPath();

        animator.SetBool("Run", false);
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;
    }



    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
    }
}
