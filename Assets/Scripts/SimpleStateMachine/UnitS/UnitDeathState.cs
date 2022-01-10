using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class UnitDeathState : AState
{
    [SerializeField] private float timeToDie;
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private Collider coll;

    private BattleUnit _currentUnit;
    private NavMeshAgent _navMesh;

    public override StateType StateType => StateType.Die;


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
        _currentUnit = _stateController.UnitController.CurrentUnit;
        _navMesh = _stateController.UnitController.NavAgent;
        coll.enabled = false;
        _navMesh.enabled = false;
        Die();
    }

    private void Die()
    {
        unitAnimator.SetTrigger("Die");
        StartCoroutine(Dying(timeToDie));
    }

    private IEnumerator Dying(float time)
    {
        yield return new WaitForSeconds(time);
        _currentUnit.transform.DOMoveY(-1, 1).OnComplete(() => Destroy(_currentUnit.gameObject));
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
