using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitChaseState : AState
{
    [SerializeField, Range(1, 100)] private float detectionRadius;
    [SerializeField, Range(1, 100)] private float attackDistance;
    [SerializeField] private Animator unitAnimator;

    private NavMeshObstacle _navObstacle;
    private NavMeshAgent _navAgent;
    private Transform _target;
    private UnitTeam _thisUnitTeam;

    private bool _hasTarget;

    public override StateType StateType => StateType.UnitChase;

    public override void Init(ASimpleStateController stateController)
    {
        _stateController = stateController;
    }

    public override void StartState()
    {
        stateCondition = StateCondition.Executing;
        _target = _stateController.UnitController.ChaseTarget;
        LocalInit();

    }

    private void LocalInit()
    {
        _navAgent = _stateController.UnitController.NavAgent;
        _navObstacle = _stateController.UnitController.NavObstacle;
        _thisUnitTeam = _stateController.UnitController.CurrentUnit.MyTeam;

        _navAgent.enabled = true;
        _navObstacle.enabled = true;
    }

    public override void Execute()
    {
        if (stateCondition != StateCondition.Executing)
            return;

        if (GameController.Instance.CurrentState == GameState.Win || GameController.Instance.CurrentState == GameState.Lose)
            _stateController.ChangeState(StateType.Idle);


        Collider[] enemyTargets = Physics.OverlapSphere(transform.position, detectionRadius);
        if (enemyTargets.Length > 0)
        {
            for (int i = 0; i < enemyTargets.Length; i++)
            {
                if (DetectedEnemyObject(enemyTargets[i], out var enemy))
                {
                    SetTarget(enemy.Transform.position);
                    Vector3 dist = enemy.Transform.position - transform.position;
                    if (dist.magnitude <= attackDistance)
                    {
                         var attackState = (UnitAttackState)_stateController.GetState(StateType.UnitAttack);
                         _stateController.ChangeState(StateType.UnitAttack);
                         attackState.SetTarget(enemy.Transform);
                    }
                }
                else
                    SetTarget(_target.position);
            }
        }
    }
   

    private void SetTarget(Vector3 target)
    {
        if (_navAgent.enabled)
        {
            _navAgent.destination = target;
            unitAnimator.SetBool("Run", true);
        }
    }


    private bool DetectedEnemyObject(Collider coll, out IBattleUnit enemy)
    {
        var enemyInParent = coll.GetComponentInParent<IBattleUnit>();
        var enemyInChildren = coll.GetComponentInChildren<IBattleUnit>();

        if(!_stateController.UnitController.IngoreUnitAndTower)
        {
            if (enemyInParent != null && enemyInParent.TeamObject.MyTeam != _thisUnitTeam && enemyInParent.IsSpotable)
            {
                enemy = enemyInParent;
                return true;
            }

            if (enemyInChildren != null && enemyInChildren.TeamObject.MyTeam != _thisUnitTeam && enemyInChildren.IsSpotable)
            {
                enemy = enemyInChildren;
                return true;
            }
        }

        if (enemyInParent != null && enemyInParent.TeamObject.MyTeam != _thisUnitTeam && enemyInParent.IsSpotable &&
                 enemyInParent.Type == UnitType.Builder)
        {
            enemy = enemyInParent;
            return true;
        }
       

        enemy = null;
        return false;
    }    
      

    public override void Stop()
    {
        stateCondition = StateCondition.Stopped;
        _navAgent.ResetPath();
        _navAgent.enabled = false;
        _navObstacle.enabled = false;
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
