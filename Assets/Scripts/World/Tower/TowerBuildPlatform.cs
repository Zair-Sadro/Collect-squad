﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using System.Threading.Tasks;

public class TowerBuildPlatform : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TowerUI towerUI;

    [Header("Precreate tower")]
    [SerializeField] private bool isPrecreateTower;
    [SerializeField] private UnitType precreatedTowerType;

    [Header("Build Settings")]
    [SerializeField] private Collider coll;
    [SerializeField] private float firstUnitInvincibilityTime;
    [SerializeField, Min(0)] private float resetTime;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float timeToDestroyByUnits;
    [SerializeField] private UnityEvent OnBuildEvent;
    [SerializeField] private UnityEvent OnDestroyEvent;
    [SerializeField] private UnityEvent OnDestroyByUnitsEvent;
    [SerializeField] private UnityEvent OnClearPlatformEvent;

    [Header("Unit Target Tower")]
    [SerializeField] private TowerBuildPlatform oppositeTower;
    [SerializeField] private UnitTeam currentTeam;
    [SerializeField] private Transform chaseTarget;
    [SerializeField] private Transform playerEnemyTarget;


    [SerializeField] private List<ATowerObject> towers = new List<ATowerObject>();


    public event Action<TowerBuildPlatform> OnTowerBuild;
    public event Action OnClearPlatform;

    public event Action<int> OnTilesIncrease;
    public event Action OnNotEnoughTiles;
    public event Action OnMaxLevelTowerReach;

    private ATowerObject _previousTower;
    private ATowerObject _activeTower;

    private bool _isTowerBuild;
    private bool _wasDestroyed;

    private int _tilesToUpgrade;
    private int _currentTiles;

    #region Properties

    public float FirstUnitInvincibilityTime => firstUnitInvincibilityTime;
    public UnityEvent DestroyEvent => OnDestroyEvent;
    public UnityEvent DestroyByUnits => OnDestroyByUnitsEvent;
    public float TimeToDestroyByUnits => timeToDestroyByUnits;
    public float TimeToDestroy => timeToDestroy;
    public TowerUI TowerUI => towerUI;
    public TowerBuildPlatform OppositeTower => oppositeTower;
    public Transform PlayerEnemyTarget => playerEnemyTarget;
    public ATowerObject ActiveTower => _activeTower;
    public UnitTeam CurrentTeam => currentTeam;
    public Transform EnemyTower => chaseTarget;
    public int CurrentTiles => _currentTiles;
    public int TilesToUpgrade => _tilesToUpgrade;
    public bool IsTowerBuild => _isTowerBuild;

    #endregion

    private void OnEnable()
    {
        InitTowers();
    }

    private void InitTowers()
    {
        for (int i = 0; i < towers.Count; i++)
            towers[i].Init(this);
    }

    private void Start()
    {
        CreateBuildPlatform();
    }

    private  void CreateBuildPlatform()
    {
        var platform = towers.Where(t => t.Data.Type == UnitType.Tower).FirstOrDefault();
        BuiltTower(platform);

        if (isPrecreateTower)
            PrebuildTower(precreatedTowerType);
    }

    public void PrebuildTower(UnitType type)
    {
        DisablePreviousTower(_activeTower);
        CreateNewTower(type);
        OnTowerBuild?.Invoke(this);
        OnBuildEvent?.Invoke();
    }

    private void IncreaseTilesAmount()
    {
        if (_tilesToUpgrade == 0)
        {
            towerUI.ToggleCounter(false);
            return;
        }

        _tilesToUpgrade--;

        towerUI.SetTilesCounter(_tilesToUpgrade, _activeTower.CurrentLevel.IsMaxLevel);
        OnTilesIncrease?.Invoke(_currentTiles);
    }

    private void TryToUpgradeTower()
    {
        bool canUpgrade = _activeTower.CurrentLevel.IsUpgradeable && !_activeTower.CurrentLevel.IsMaxLevel;

        if (canUpgrade)
        {
            var nextLevelTower = _activeTower as TowerObject;

            if (_activeTower.CurrentLevel.IsMaxLevel)
            {
                OnMaxLevelTowerReach?.Invoke();
                return;
            }
            else
                BuiltTower(nextLevelTower.NextLevelTower);
        }
    }

    public void BuiltTower(UnitType type)
    {
        if (_currentTiles < _tilesToUpgrade)
        {
            OnNotEnoughTiles?.Invoke();
            return;
        }
        DisablePreviousTower(_activeTower);
        CreateNewTower(type);
        StartCoroutine(ResetTilesGet(resetTime));
        _isTowerBuild = true;
        OnTowerBuild?.Invoke(this);
        OnBuildEvent?.Invoke();
    }

    public void BuiltTower(ATowerObject tower)
    {
        if (_currentTiles < _tilesToUpgrade)
        {
            OnNotEnoughTiles?.Invoke();
            return;
        }
        DisablePreviousTower(_activeTower);
        CreateNewTower(tower);
        StartCoroutine(ResetTilesGet(resetTime));
        OnTowerBuild?.Invoke(this);
        OnBuildEvent?.Invoke();
    }

    private void CreateNewTower(UnitType type)
    {
        ATowerObject tower = towers.Where(t => t.Data.Type == type).FirstOrDefault();
        tower.gameObject.SetActive(true);
        ResetTilesCounter(tower.CurrentLevel);
        _activeTower = tower;
    }

    private void CreateNewTower(ATowerObject tower)
    {
        tower.gameObject.SetActive(true);
        ResetTilesCounter(tower.CurrentLevel);
        _activeTower = tower;
    }

    private void ResetTilesCounter(TowerLevel towerLevel)
    {
        _currentTiles = 0;
        _tilesToUpgrade = towerLevel.TilesToUpgrade;
        towerUI.SetTilesCounter(_tilesToUpgrade, towerLevel.IsMaxLevel);
    }

    public void DestroyTower(float time, UnityEvent destroyEvent)
    {
        StartCoroutine(Destroying(time, destroyEvent));
    }

    private IEnumerator Destroying(float time, UnityEvent destroyEvent)
    {
        destroyEvent?.Invoke();
        yield return new WaitForSeconds(time);
        var platform = towers.Where(t => t.Data.Type == UnitType.Tower).FirstOrDefault();

        _activeTower.gameObject.SetActive(false);
        _previousTower = null;
        _activeTower = platform;
        platform.gameObject.SetActive(true);
        ResetTilesCounter(platform.CurrentLevel);
        _isTowerBuild = false;
        _wasDestroyed = true;
        OnClearPlatform?.Invoke();
        yield return new WaitForSeconds(1);
        OnClearPlatformEvent?.Invoke();
    }

    private void DisablePreviousTower(ATowerObject tower)
    {
        _previousTower = tower;
        if (_previousTower != null)
            _previousTower.gameObject.SetActive(false);
    }    


    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out TileSetter tileSetter))
        {

            if (tileSetter.IsThisBot)
                TryBuildRandomTower();

            if (_tilesToUpgrade <= 0)
            {
                towerUI.ToggleCounter(false);
                TryToUpgradeTower();
                tileSetter.StopRemovingTiles();
                return;
            }

            tileSetter.RemoveTiles(IncreaseTilesAmount, this);
        }
    }

    private void TryBuildRandomTower()
    {
        if (_activeTower.CurrentLevel.LevelType > 0 && _activeTower != null)
            return;

        if(_tilesToUpgrade == 0 && _activeTower.CurrentLevel.LevelType == 0)
        {
            var randTower = towers.Where(t => t.CurrentLevel.LevelType == TowerLevelType.level1).ToList();
            BuiltTower(randTower[Random.Range(0, randTower.Count)]);
            _isTowerBuild = true;
        }
    }

    private IEnumerator ResetTilesGet(float time)
    {
        coll.enabled = false;
        yield return new WaitForSeconds(time);
        coll.enabled = true;
    }

    public void SetTargetToBuilder()
    {
        chaseTarget.parent = playerEnemyTarget;
        chaseTarget.transform.localPosition = Vector3.zero;
    }

    public void StopTowersActivity()
    {
        foreach (var t in towers)
            t.StopAllCoroutines();
    }

    private void OnDrawGizmosSelected()
    {
        if(chaseTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, chaseTarget.localPosition);
            Gizmos.DrawSphere(chaseTarget.transform.localPosition, 0.2f);
        }

        if(oppositeTower != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, oppositeTower.transform.position);
        }
    }

}
