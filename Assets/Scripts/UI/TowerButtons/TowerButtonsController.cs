using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonsController : MonoBehaviour
{
    [SerializeField] private TileSetter playerTileSetter;
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private DestroyTowerButton destroyTowerButton;
    [SerializeField] private TowerSwapContent swapContent;
    [SerializeField] private List<TowerUI> towersUI = new List<TowerUI>();

    private TowerBuildPlatform _towerToSwap;

    private void OnEnable()
    {
        playerTileSetter.OnBuildZoneEnter += BuildZoneEnter;
        playerTileSetter.OnBuildZoneExit += BuildZoneExit;

        playerTileSetter.OnSwapZoneEnter += SwapZoneEnter;
        playerTileSetter.OnSwapZoneExit += SwapZoneExit;
    }

   

    private void OnDisable()
    {
        playerTileSetter.OnBuildZoneEnter -= BuildZoneEnter;
        playerTileSetter.OnBuildZoneExit -= BuildZoneExit;

        playerTileSetter.OnSwapZoneEnter -= SwapZoneEnter;
        playerTileSetter.OnSwapZoneExit -= SwapZoneExit;
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

    private void SwapZoneExit()
    {
        swapContent.gameObject.SetActive(false);
       // destroyTowerButton.gameObject.SetActive(false);
    }

    private void SwapZoneEnter(SwapZone zone)
    {
        _towerToSwap = zone.MainTower;

        if (_towerToSwap.IsTowerBuild)
        {
            _towerToSwap.OnClearPlatform += MainTower_OnClearPlatform;
            swapContent.Init(_towerToSwap, playerTileSetter);
            swapContent.gameObject.SetActive(true);
           // destroyTowerButton.gameObject.SetActive(true);
           // destroyTowerButton.Init(_bombTower);
        }
    }

    private void MainTower_OnClearPlatform()
    {

        swapContent.gameObject.SetActive(false);
        _towerToSwap.OnClearPlatform -= MainTower_OnClearPlatform;
        //  destroyTowerButton.gameObject.SetActive(false);
    }

    private void OnTowerBuild(TowerBuildPlatform currentBuildPlatform)
    {
        BuildZoneExit();
        currentBuildPlatform.OnTowerBuild -= OnTowerBuild;
    }
}
