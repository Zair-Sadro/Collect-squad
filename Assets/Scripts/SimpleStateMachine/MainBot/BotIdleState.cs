using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotIdleState : AState
{
    [SerializeField] private AnimationClip animClip;

    private NavMeshAgent _navAgent;
    private Animator _animator;
    private BotStateController _botController;

    public override StateType StateType => StateType.Idle;


    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
        _botController = _stateController.BotController;
        _animator = _botController.Animator;
        _navAgent = _botController.NavAgent;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        _animator.Play(animClip.name);

        if (_navAgent.enabled)
            _navAgent.ResetPath();
        
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
