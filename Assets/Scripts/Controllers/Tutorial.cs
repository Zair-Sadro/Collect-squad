using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TileSetter playerTileSetter;
    [SerializeField] private TowerBuildPlatform enemyTower;
    [SerializeField] private BotStateController _botController;

    [SerializeField] private GameObject tileArrow;
    [SerializeField] private GameObject towerArrow;
    [SerializeField] private GameObject tapHand;


    private int _tileCount;
    private TowerBuildPlatform _currentTower;

    private void OnEnable()
    {
        playerTileSetter.OnTilesCountChanged += OnPlayerTiles;
        playerTileSetter.OnBuildZoneEnter += OnBuildZoneEnter;
        playerTileSetter.OnBuildZoneExit += OnBuildZoneExit;
    }

    private void OnDisable()
    {
        playerTileSetter.OnTilesCountChanged -= OnPlayerTiles;
        playerTileSetter.OnBuildZoneEnter -= OnBuildZoneEnter;
        playerTileSetter.OnBuildZoneExit -= OnBuildZoneExit;
    }

    private void OnBuildZoneEnter(TowerBuildPlatform tower)
    {
        _currentTower = tower;
        _currentTower.OnTowerBuild += OnTowerBuild;
        towerArrow.SetActive(false);

        if (_currentTower.TilesToUpgrade == 0)
            tapHand.SetActive(true);
    }

    private void OnTowerBuild(TowerBuildPlatform obj)
    {
        enemyTower.PrebuildTower(UnitType.Spear);
        towerArrow.SetActive(false);
        _botController.enabled = true;

        _currentTower.OnTowerBuild -= OnTowerBuild;

        if(_currentTower.ActiveTower.CurrentLevel.LevelType > 0)
        {
            _currentTower.OnTowerBuild -= OnTowerBuild;
            playerTileSetter.OnTilesCountChanged -= OnPlayerTiles;
            playerTileSetter.OnBuildZoneEnter -= OnBuildZoneEnter;
            playerTileSetter.OnBuildZoneExit -= OnBuildZoneExit;

            tapHand.SetActive(false);
            towerArrow.SetActive(false);
        }
    }

    private void OnBuildZoneExit()
    {
        if(_currentTower.ActiveTower.CurrentLevel.LevelType == 0)
            towerArrow.SetActive(true);

        tapHand.SetActive(false);
    }


    private void OnPlayerTiles(int count)
    {
        _tileCount = count;

        if (_tileCount > 0)
            tileArrow.SetActive(false);

        if(_tileCount >= 10)
        {
            tileArrow.SetActive(false);
            towerArrow.SetActive(true);

            playerTileSetter.OnTilesCountChanged -= OnPlayerTiles;
        }

    }
}
