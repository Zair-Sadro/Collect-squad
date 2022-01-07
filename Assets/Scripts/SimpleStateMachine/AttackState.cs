using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AState
{
    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        Debug.Log("Starting state" + this.gameObject.name);
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            _stateController.ChangeState(StateType.Chase);
    }


    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
        Debug.Log("Stoping state" + this.gameObject.name);
    }
}
