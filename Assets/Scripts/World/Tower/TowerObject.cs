using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TowerObject : ATowerObject, IDamageable, ITeamChangeable, IBattleUnit
{
    [Header("Tower settings")]
    [SerializeField, Range(1,1000)] private float maxHp;
    [SerializeField] private ATowerObject nextLevelTower;
    [Header("Units Settings")]
    [SerializeField, Min(0)] private float firstUnitSpawnTime;
    [SerializeField, Min(0)] private float spawnTime;
    [SerializeField] private Transform spawnPoint;

    private float _currentHp;
    private UnitTeam _currentTeam;

    #region Properties

    public ITeamChangeable TeamObject => this;
    public UnitTeam MyTeam => _currentTeam;
    public ATowerObject NextLevelTower => nextLevelTower;
    public Transform Transform => transform;
    public UnitType Type => UnitType.Tower;
    public IDamageable Damageable => this;

    #endregion

    public override void Init(TowerBuildPlatform buildPlatform)
    {
        base.Init(buildPlatform);
        _currentHp = maxHp;
        _currentTeam = buildPlatform.CurrentTeam;
    }

    private void OnEnable()
    {
        StartSpawn(firstUnitSpawnTime);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void StartSpawn(float time)
    {
        StartCoroutine(UnitSpawning(time));
    }

    private IEnumerator UnitSpawning(float time)
    {
        yield return new WaitForSeconds(time);
        BattleUnit newUnit = Instantiate(CurrentLevel.UnitPrefab, this.transform);
        newUnit.transform.localPosition = spawnPoint.localPosition;
        newUnit.transform.parent = null;
        newUnit.Init(_currentBuildPlatform.EnemyTower, _currentTeam);
        StartSpawn(spawnTime);
    }

    public void TakeDamage(float amount)
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
