﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;

public class TowerObject : ATowerObject, IDamageable, ITeamChangeable, IBattleUnit
{
    [Header("Tower settings")]
    [SerializeField, Range(1, 1000)] private float maxHp;
    [SerializeField] private ATowerObject nextLevelTower;
    [Header("Units Settings")]
    [SerializeField, Min(0)] private float firstUnitSpawnTime;
    [SerializeField, Min(0)] private float spawnTime;
    [SerializeField] private int maxUnitsAlive;
    [SerializeField] private Vector3 spawnRotation;
    [SerializeField] private Transform spawnPoint;

    private bool _wasDestroyed;
    private bool _isRespawningUnit;
    private int _currentUnitsAmount;
    private float _firstUnitInvinciblityTime;
    private float _currentHp;
    private UnitTeam _currentTeam;
    private TowerUI _towerUI;


    private List<BattleUnit> _spawnedUnits = new List<BattleUnit>();

    public event Action<float, float> OnGetDamaged;
    public event Action<TowerObject> OnCurrentTowerDestroy;

    #region Properties

    public float CurrentHealth => _currentHp;
    public float MaxHealth => maxHp;
    public float InvincibilityTime => _firstUnitInvinciblityTime;
    public bool WasDestroyed => _wasDestroyed;
    public int CurrentUnitsAmount { get => _currentUnitsAmount; set => _currentUnitsAmount = value; }
    public ITeamChangeable TeamObject => this;
    public UnitTeam MyTeam => _currentTeam;
    public ATowerObject NextLevelTower => nextLevelTower;
    public Transform Transform => transform;
    public UnitType Type => UnitType.Tower;
    public IDamageable Damageable => this;
    public float SpawnTime => spawnTime;
    public bool IsSpotable => true;
    public bool IsDamageable => true;

    #endregion

    public override void Init(TowerBuildPlatform buildPlatform)
    {
        base.Init(buildPlatform);
        _currentHp = maxHp;
        _currentTeam = buildPlatform.CurrentTeam;
        _towerUI = buildPlatform.TowerUI;
        _firstUnitInvinciblityTime = buildPlatform.FirstUnitInvincibilityTime;
    }

    private void OnEnable()
    {
        _currentUnitsAmount = 0;
        StartSpawn(firstUnitSpawnTime);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void StartSpawn(float time)
    {
        _towerUI.StartSpawnTimer(spawnTime);
        StartCoroutine(UnitSpawning(time));
    }

    private IEnumerator UnitSpawning(float time)
    {
        _isRespawningUnit = true;
        yield return new WaitForSeconds(time);
        BattleUnit newUnit = Instantiate(CurrentLevel.GetUnitByTeam(_currentTeam), this.transform);
        _currentUnitsAmount++;
        newUnit.transform.localPosition = spawnPoint.localPosition;
        newUnit.transform.localRotation = Quaternion.Euler(spawnRotation);
        newUnit.transform.parent = null;
        newUnit.Init(_currentBuildPlatform.EnemyTower, _currentTeam, this);
        _isRespawningUnit = false;
        TryRespawnNextUnit();
    }

    public void TryRespawnNextUnit()
    {
       if(_currentUnitsAmount < maxUnitsAlive && !_isRespawningUnit)
       {
            _towerUI.StartSpawnTimer(spawnTime);
            StartCoroutine(UnitSpawning(spawnTime));
       }
    }

    public void TakeDamage(float amount)
    {
        _currentHp -= amount;
        OnGetDamaged?.Invoke(_currentHp, maxHp);
        transform.DORewind();
        transform.DOShakeScale(0.1f, 0.05f);

        if (_currentHp < 0)
            DestroyTower();
    }

    private void DestroyTower()
    {
        var player = GameController.Instance.Player.GetComponent<BuilderUnit>();
        if (player.MyTeam != _currentTeam)
            GameController.AddSessionCoins(10);

        Vibration.Vibrate(25);
        _wasDestroyed = true;
        OnCurrentTowerDestroy?.Invoke(this);
        _currentBuildPlatform.DestroyTower(_currentBuildPlatform.TimeToDestroyByUnits, _currentBuildPlatform.DestroyByUnits);
    }

}
