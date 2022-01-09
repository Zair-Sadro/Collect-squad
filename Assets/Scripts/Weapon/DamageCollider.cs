using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private AWeapon weapon;
    [SerializeField] private BattleUnit _currentUnit;

    private void OnTriggerEnter(Collider other)
    {
       if(DetectedEnemyObject(other, out var enemy))
       {
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

        if (enemyInParent != null && enemyInParent.TeamObject.MyTeam != _currentUnit.MyTeam)
        {
            enemy = enemyInParent;
            return true;
        }

        if (enemyInChildren != null && enemyInChildren.TeamObject.MyTeam != _currentUnit.MyTeam)
        {
            enemy = enemyInChildren;
            return true;
        }

        enemy = null;
        return false;
    }
    
}
