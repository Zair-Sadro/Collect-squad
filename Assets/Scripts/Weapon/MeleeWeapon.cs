using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : AWeapon
{
    [SerializeField] private DamageCollider damageColl;

    public override WeaponType WeaponType => WeaponType.Melee;

    public override void Attack()
    {
        if(State == WeaponState.Serenity)
        {
            StopAllCoroutines();
            StartCoroutine(Attacking(WeaponSettings.TimeToAttack));
        }
    }

    protected override IEnumerator Attacking(float time)
    {
         State = WeaponState.Attack;
        if(State == WeaponState.Attack)
        {
            yield return new WaitForSeconds(time);
            damageColl.gameObject.SetActive(true);
            StartCoroutine(Reload(WeaponSettings.DamageTime));
        }
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (State == WeaponState.Reload)
        {
            yield return new WaitForSeconds(time);
            damageColl.gameObject.SetActive(false);
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
