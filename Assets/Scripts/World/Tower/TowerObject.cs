using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TowerObject : ATowerObject, IDamageable
{
    [Header("Tower settings")]
    [SerializeField, Range(1,1000)] private int maxHp;
    [SerializeField] private ATowerObject nextLevelTower;
    [Header("Units Settings")]
    [SerializeField] private Transform spawnPoint;

    private int _currentHp;

    public ATowerObject NextLevelTower => nextLevelTower;

    public override void Init()
    {
        base.Init();
        _currentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        _currentHp -= amount;
        if (_currentHp < 0)
            DestroyTower();
    }

    private void DestroyTower()
    {
        
    }
}
