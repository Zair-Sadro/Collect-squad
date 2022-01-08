using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitChaseState : AState
{
    [SerializeField, Range(1, 100)] private float detectionRadius;
    [SerializeField, Range(1, 100)] private float attackDistance;
    [SerializeField] private Animator unitAnimator;

    private NavMeshAgent _navAgent;
    private Transform _target;
    private UnitTeam _thisUnitTeam;

    public override StateType StateType => StateType.Chase;

    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        Debug.Log("Starting state" + this.gameObject.name);
        stateCondition = StateCondition.Executing;
        LocalInit();

    }

    private void LocalInit()
    {
        _navAgent = _stateController.UnitController.NavAgent;
        _target = _stateController.UnitController.ChaseTarget;
        _thisUnitTeam = _stateController.UnitController.CurrentUnit.MyTeam;
        _navAgent.enabled = true;

        unitAnimator.SetBool("Run", true);
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        Collider[] enemyTarget = Physics.OverlapSphere(transform.position, detectionRadius);
        if (enemyTarget.Length > 0)
        {
            if (DetectedEnemyObject(enemyTarget[0], out var enemy))
            {
                _navAgent.SetDestination(enemy.Transform.position);
                Vector3 dist = enemy.Transform.position - transform.position;
                if (dist.magnitude <= attackDistance)
                {
                    var attackState = (UnitAttackState)_stateController.GetState(StateType.Attack);
                    _stateController.ChangeState(StateType.Attack);
                    attackState.AttackingTarget = enemy.Transform;
                }
            }
            else
                _navAgent.destination = _target.position;
        }
    }

    private bool DetectedEnemyObject(Collider coll, out ITeamChangeable enemy)
    {
        var enemyInParent = coll.GetComponentInParent<ITeamChangeable>();
        var enemyInChildren = coll.GetComponentInChildren<ITeamChangeable>();

        if (enemyInParent != null && enemyInParent.MyTeam != _thisUnitTeam)
        {
            enemy = enemyInParent;
            return true;
        }

        if(enemyInChildren != null && enemyInChildren.MyTeam != _thisUnitTeam)
        {
            enemy = enemyInChildren;
            return true;
        }

        enemy = null;
        return false;
    }    
      

    public override void Stop()
    {
        Debug.Log("Stoping state" + this.gameObject.name);
        stateCondition = StateCondition.Stopped;
        unitAnimator.SetBool("Run", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * attackDistance);
    }

}
