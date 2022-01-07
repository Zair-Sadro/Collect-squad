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
    [SerializeField, Min(0)] private float spawnTime;
    [SerializeField] private Transform spawnPoint;

    private int _currentHp;

    public ATowerObject NextLevelTower => nextLevelTower;

    public override void Init(TowerBuildPlatform buildPlatform)
    {
        base.Init(buildPlatform);
        _currentHp = maxHp;
    }

    private void OnEnable()
    {
        StartSpawn();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void StartSpawn()
    {
        StartCoroutine(UnitSpawning(spawnTime));
    }

    private IEnumerator UnitSpawning(float time)
    {
        yield return new WaitForSeconds(time);
        BattleUnit newUnit = Instantiate(CurrentLevel.UnitPrefab, this.transform);
        newUnit.transform.localPosition = spawnPoint.localPosition;
        newUnit.Init(_currentBuildPlatform.EnemyTower);
        StartSpawn();
    }

    public void TakeDamage(int amount)
    {
        _currentHp -= amount;
        if (_currentHp < 0)
            DestroyTower();
    }

    private void DestroyTower()
    {
        _currentBuildPlatform.DestroyTower();
    }
}
