using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private AWeapon weapon;
    [SerializeField] private BattleUnit currentUnit;
    [SerializeField] private UnityEvent OnColliderEnter;

    public void Init(AWeapon weapon, BattleUnit unit)
    {
        this.weapon = weapon;
        currentUnit = unit;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(DetectedEnemyObject(other, out var enemy))
       {
            OnColliderEnter?.Invoke();
            for (int i = 0; i < weapon.Damages.Count; i++)
            {
                if (enemy.Type == weapon.Damages[i].Type)
                    enemy.Damageable.TakeDamage(weapon.Damages[i].DamageToType);
            }
       }

    }

    private bool DetectedEnemyObject(Collider coll, out IBattleUnit enemy)
    {

        var enemyInParent = coll.GetComponentInParent<IBattleUnit>();
        var enemyInChildren = coll.GetComponentInChildren<IBattleUnit>();

        if (enemyInParent != null && enemyInParent.TeamObject.MyTeam != currentUnit.MyTeam)
        {
            enemy = enemyInParent;
            return true;
        }

        if (enemyInChildren != null && enemyInChildren.TeamObject.MyTeam != currentUnit.MyTeam)
        {
            enemy = enemyInChildren;
            return true;
        }

        enemy = null;
        return false;
    }
    
}
