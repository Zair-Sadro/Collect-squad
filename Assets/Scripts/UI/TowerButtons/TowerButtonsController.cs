using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonsController : MonoBehaviour
{
    [SerializeField] private TileSetter playerTileSetter;
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private DestroyTowerButton destroyTowerButton;
    [SerializeField] private List<TowerUI> towersUI = new List<TowerUI>();

    private TowerBuildPlatform _bombTower;

    private void OnEnable()
    {
        playerTileSetter.OnBuildZoneEnter += BuildZoneEnter;
        playerTileSetter.OnBuildZoneExit += BuildZoneExit;

        playerTileSetter.OnBombZoneEnter += BombZoneEnter;
        playerTileSetter.OnBombZoneExit += BombZoneExit;
    }

   

    private void OnDisable()
    {
        playerTileSetter.OnBuildZoneEnter -= BuildZoneEnter;
        playerTileSetter.OnBuildZoneExit -= BuildZoneExit;

        playerTileSetter.OnBombZoneEnter -= BombZoneEnter;
        playerTileSetter.OnBombZoneExit -= BombZoneExit;
    }

    private void BuildZoneExit()
    {
        foreach (var b in towersUI)
        {
            b.ButtonsUnsub();
            b.ToogleButtons(false);
        }
    }

    private void BuildZoneEnter(TowerBuildPlatform currentBuildPlatform)
    {
        currentBuildPlatform.OnTowerBuild += OnTowerBuild;

        if (currentBuildPlatform.IsTowerBuild)
            return;


        if(currentBuildPlatform.TilesToUpgrade == 0)
        {
            for (int i = 0; i < towersUI.Count; i++)
            {
                if(currentBuildPlatform.TowerUI == towersUI[i])
                {
                    towersUI[i].ToogleButtons(true);
                    towersUI[i].InitButtons(currentBuildPlatform);
                }
            }

           // foreach (var b in towersUI)
           // {
           //     b.ToogleButtons(true);
           //     b.InitButtons(currentBuildPlatform);
           // }
        }
    }

    private void BombZoneExit()
    {
        destroyTowerButton.gameObject.SetActive(false);
    }

    private void BombZoneEnter(BombZone zone)
    {
        _bombTower = zone.MainTower;

        if (_bombTower.IsTowerBuild)
        {
            _bombTower.OnClearPlatform += MainTower_OnClearPlatform;
            destroyTowerButton.gameObject.SetActive(true);
            destroyTowerButton.Init(_bombTower);
        }
    }

    private void MainTower_OnClearPlatform()
    {
        destroyTowerButton.gameObject.SetActive(false);
        _bombTower.OnClearPlatform -= MainTower_OnClearPlatform;
    }

    private void OnTowerBuild(TowerBuildPlatform currentBuildPlatform)
    {
        BuildZoneExit();
        currentBuildPlatform.OnTowerBuild -= OnTowerBuild;
    }
}
