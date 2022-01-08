﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTeam
{
    Team1, Team2
}

public class BattleUnit : MonoBehaviour, IDamageable, ITeamChangeable, IBattleUnit
{
    [SerializeField] private UnitType type;
    [SerializeField,Range(1,100)] private float health;
    [SerializeField,Range(1,100)] private float moveSpeed;
    [SerializeField, Min(0)] private float timeToDie;
    [SerializeField] private UnitStateController stateMachine;

    private UnitTeam _team;
    private Transform _towerTarget;
    private float _currentHealth;

    #region Properties

    public ITeamChangeable TeamObject => this;
    public IDamageable Damageable => this;
    public UnitTeam MyTeam => _team;
    public Transform TowerTarget => _towerTarget;
    public Transform Transform => transform;
    public UnitType Type => type;
    public float Health => health;
    public float MoveSpeed => moveSpeed;


    #endregion

    public void Init(Transform towerToAttack, UnitTeam team)
    {
        _towerTarget = towerToAttack;
        _team = team;
        _currentHealth = health;
        stateMachine.Init(this);
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        StartCoroutine(Dying(timeToDie));
    }

    private IEnumerator Dying (float dieTime)
    {
        //play death anim
        yield return new WaitForSeconds(dieTime);
        //destroy gameobj
    }

    
}

