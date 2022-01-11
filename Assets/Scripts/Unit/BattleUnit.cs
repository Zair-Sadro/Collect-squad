using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum UnitTeam
{
    Team1, Team2
}

public class BattleUnit : MonoBehaviour, IDamageable, ITeamChangeable, IBattleUnit
{
    [SerializeField] private UnitType type;
    [SerializeField,Range(1,100)] private float health;
    [SerializeField,Range(0.1f,10)] private float moveSpeed;
    [SerializeField] private UnitStateController stateMachine;

    private UnitTeam _team;
    private Transform _attackTarget;
    private float _currentHealth;
    private TowerObject _tower;

    #region Properties

    public ITeamChangeable TeamObject => this;
    public IDamageable Damageable => this;
    public UnitTeam MyTeam => _team;
    public Transform TowerTarget => _attackTarget;
    public Transform Transform => transform;
    public UnitType Type => type;
    public float Health => health;
    public float MoveSpeed => moveSpeed;
    public bool IsSpotable => true;


    #endregion

    public void Init(Transform attackTarget, UnitTeam team, TowerObject tower)
    {
        _attackTarget = attackTarget;
        _tower = tower;
        _team = team;
        _currentHealth = health;
        stateMachine.Init(this);
    }

    public void FinishInit(Transform finishTarget, float speed)
    {
        _attackTarget = finishTarget;
        moveSpeed = speed;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        _tower.CurrentUnitsAmount--;
        stateMachine.ChangeState(StateType.Die);
    }

   

    
}

