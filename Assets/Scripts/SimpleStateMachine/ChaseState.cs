using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : AState
{
    private NavMeshAgent _navAgent;
    private Transform _target;


    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        _navAgent = _stateController.UnitController.NavAgent;
        _target = _stateController.UnitController.ChaseTarget;
        _navAgent.enabled = true;
        Debug.Log("Starting state" + this.gameObject.name);
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            _stateController.ChangeState(StateType.Attack);

        _navAgent.SetDestination(_target.position);

        if(Vector3.Distance(transform.position, _target.position) < 5)
            _stateController.ChangeState(StateType.Attack);

    }


    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
        Debug.Log("Stoping state" + this.gameObject.name);
    }
}
