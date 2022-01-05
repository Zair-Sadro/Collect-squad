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
    [SerializeField, Min(0)] private int tilesToBuildFirstTower;

    [SerializeField] private List<TowerObject> towers = new List<TowerObject>();
    

    public event Action<int> OnTilesIncrease;
    public event Action OnNotEnoughTiles;

    private bool _canGetTiles = true;
    private bool _isTowerBuild;
    private int _currentTiles;

    #region Properties

    public bool CanGetTiles => _canGetTiles;
    public int CurrentTiles => _currentTiles;
    public bool IsTowerBuild => _isTowerBuild;

    #endregion


    private void OnEnable()
    {
        towerUI.SetTilesCounter(_currentTiles, tilesToBuildFirstTower);
    }

    private void IncreaseTilesAmount()
    {
        if (_currentTiles >= tilesToBuildFirstTower)
        {
            _canGetTiles = false;
            return;
        }

        _currentTiles++;
        towerUI.SetTilesCounter(_currentTiles, tilesToBuildFirstTower);
        OnTilesIncrease?.Invoke(_currentTiles);
    }

    public void BuiltTower(UnitType type)
    {
        if (_currentTiles < tilesToBuildFirstTower)
        {
            OnNotEnoughTiles?.Invoke();
            return;
        }

        TowerObject tower = towers.Where(t => t.Data.Type == type).FirstOrDefault();
        tower.gameObject.SetActive(true);
        tower.Init();
        StartCoroutine(ResetTilesGet(resetTime));
        _isTowerBuild = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out TileSetter tileSetter))
            tileSetter.RemoveTiles(IncreaseTilesAmount, _canGetTiles);
    }

    private IEnumerator ResetTilesGet(float time)
    {
        coll.enabled = false;
        _canGetTiles = false;
        yield return new WaitForSeconds(time);
        coll.enabled = true;
        _canGetTiles = true;
    }

}
