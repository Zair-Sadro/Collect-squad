using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] private UnitAttackState attackState;
    [SerializeField] private ArrowSpawner arrowSpawner;

    public override WeaponType WeaponType => WeaponType.Range;

    private void OnEnable()
    {
        InitArrows();
    }

    private void InitArrows()
    {

    }

    public override void Attack()
    {
        if (State == WeaponState.Serenity)
        {
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
