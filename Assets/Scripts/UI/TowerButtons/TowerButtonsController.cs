using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonsController : MonoBehaviour
{
    [SerializeField] private TileSetter playerTileSetter;
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private DestroyTowerButton destroyTowerButton;
    [SerializeField] private List<TowerUI> towersUI = new List<TowerUI>();


    private void OnEnable()
    {
        playerTileSetter.OnBuildZoneEnter += BuildZoneEnter;
        playerTileSetter.OnBuildZoneExit += BuildZoneExit;
    }

    private void OnDisable()
    {
        playerTileSetter.OnBuildZoneEnter -= BuildZoneEnter;
        playerTileSetter.OnBuildZoneExit -= BuildZoneExit;
    }

    private void BuildZoneExit()
    {
        destroyTowerButton.gameObject.SetActive(false);
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
        {
            destroyTowerButton.gameObject.SetActive(true);
            destroyTowerButton.Init(currentBuildPlatform);
            return;
        }

        if(currentBuildPlatform.TilesToUpgrade == 0)
        {
            foreach (var b in towersUI)
            {
                b.ToogleButtons(true);
                b.InitButtons(currentBuildPlatform);
            }
        }
    }

    private void OnTowerBuild(TowerBuildPlatform currentBuildPlatform)
    {
        BuildZoneExit();
        currentBuildPlatform.OnTowerBuild -= OnTowerBuild;
    }
}
