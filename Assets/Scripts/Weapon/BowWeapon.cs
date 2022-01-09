using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : AWeapon
{
    [SerializeField] private UnitAttackState attackState;
    [SerializeField] private ArrowSpawner arrowSpawner;
    [SerializeField] private BattleUnit _currentUnit;

    public override WeaponType WeaponType => WeaponType.Range;

    private void Start()
    {
        InitArrows();
    }

    private void InitArrows()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out ArrowProjectile arrow))
                arrow.Init(attackState.AttackingTarget, this, _currentUnit);
        }  
    }

    public override void Attack()
    {
        if (State == WeaponState.Serenity)
        {
            InitArrows();
            StopAllCoroutines();
            StartCoroutine(Attacking(WeaponSettings.TimeToAttack));
        }
    }

    protected override IEnumerator Attacking(float time)
    {
        State = WeaponState.Attack;
        if (State == WeaponState.Attack)
        {
            yield return new WaitForSeconds(time);
            arrowSpawner.StartSpawn();
            StartCoroutine(Reload(WeaponSettings.DamageTime));
        }
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (State == WeaponState.Reload)
        {
            yield return new WaitForSeconds(time);
            StartCoroutine(Serenity(WeaponSettings.TimeToReload));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        if (state != WeaponState.Serenity)
        {
            yield return new WaitForSeconds(time);
            State = WeaponState.Serenity;
            StopAllCoroutines();
        }
    }
}
