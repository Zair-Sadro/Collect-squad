using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class TowerBuildPlatform : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TowerUI towerUI;

    [Header("Build Settings")]
    [SerializeField] private Collider coll;
    [SerializeField, Min(0)] private float resetTime;
    

    [Header("Unit Target Tower")]
    [SerializeField] private Transform enemyTowerTarget;

    [SerializeField] private List<ATowerObject> towers = new List<ATowerObject>();
    

    public event Action<int> OnTilesIncrease;
    public event Action OnNotEnoughTiles;

    private ATowerObject _previousTower;
    private ATowerObject _activeTower;

    private bool _isTowerBuild;

    private int _tilesToUpgrade;
    private int _currentTiles;

    #region Properties

    public int CurrentTiles => _currentTiles;
    public bool IsTowerBuild => _isTowerBuild;

    #endregion


    private void OnEnable()
    {
        CreateBuildPlatform();
    }

    private void CreateBuildPlatform()
    {
        var platform = towers.Where(t => t.Data.Type == UnitType.DontMatter).FirstOrDefault();
        BuiltTower(platform);
    }

    private void IncreaseTilesAmount()
    {
        if (_currentTiles >= _tilesToUpgrade)
        {
            TryToUpgradeTower();
            return;
        }
        _currentTiles++;
        towerUI.SetTilesCounter(_currentTiles, _tilesToUpgrade);
        OnTilesIncrease?.Invoke(_currentTiles);
    }

    private void TryToUpgradeTower()
    {
        bool canUpgrade = _previousTower.CurrentLevel.LevelType != TowerLevelType.None &&
                          _previousTower.CurrentLevel.LevelType != TowerLevelType.level3;

        if(canUpgrade)
        {
            var nextLevelTower = towers.Where(t => t.Data.Type == _previousTower.Data.Type &&
                                              t.CurrentLevel.LevelType == _previousTower.CurrentLevel.LevelType + 1).FirstOrDefault();
            if(nextLevelTower)
                BuiltTower(nextLevelTower);
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
    }

    public void BuiltTower(ATowerObject tower)
    {
        if (_currentTiles < _tilesToUpgrade)
        {
            OnNotEnoughTiles?.Invoke();
            return;
        }
        DisablePreviousTower(_activeTower);
        CreateNewTower(tower.Data.Type);
        StartCoroutine(ResetTilesGet(resetTime));
        _isTowerBuild = true;
    }

    private void CreateNewTower(UnitType type)
    {
        ATowerObject tower = towers.Where(t => t.Data.Type == type).FirstOrDefault();
        tower.gameObject.SetActive(true);
        ResetTilesCounter(tower.CurrentLevel);
        _activeTower = tower;
    }

    private void ResetTilesCounter(TowerLevel towerLevel)
    {
        _currentTiles = 0;
        _tilesToUpgrade = towerLevel.TilesToUpgrade;
        towerUI.SetTilesCounter(_currentTiles, _tilesToUpgrade);
    }

    public void DestroyTower()
    {
        if(_activeTower == null)
        {
            Debug.Log("<color=yellow> Can't find tower to destroy </color>");
            return;
        }


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
            if (_currentTiles >= _tilesToUpgrade)
            {
                tileSetter.StopAllCoroutines();
                return;
            }

            tileSetter.RemoveTiles(IncreaseTilesAmount);
        }
    }

    private IEnumerator ResetTilesGet(float time)
    {
        coll.enabled = false;
        yield return new WaitForSeconds(time);
        coll.enabled = true;
    }


}
