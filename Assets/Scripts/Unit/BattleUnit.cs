using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitType type;
    [SerializeField,Range(1,100)] private float health;
    [SerializeField,Range(1,100)] private float moveSpeed;
    [SerializeField, Min(0)] private float timeToDie;
    [SerializeField] private UnitStateController stateMachine;

    private Transform _towerTarget;
    private float _currentHealth;

    #region Properties

    public Transform TowerTarget => _towerTarget;
    public UnitType Type => type;
    public float Health => health;
    public float MoveSpeed => moveSpeed;

    #endregion

    public void Init(Transform towerToAttack)
    {
        _towerTarget = towerToAttack;
        _currentHealth = health;
        stateMachine.Init(this);
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
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

