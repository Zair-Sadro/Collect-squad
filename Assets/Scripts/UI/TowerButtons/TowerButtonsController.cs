using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonsController : MonoBehaviour
{
    [SerializeField] private TileSetter playerTileSetter;
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private DestroyTowerButton destroyTowerButton;
    [SerializeField] private List<TowerButton> towerButtons = new List<TowerButton>();


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
        buttonsLayout.gameObject.SetActive(false);
        foreach (var b in towerButtons)
            b.RemoveSubs();
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

        buttonsLayout.gameObject.SetActive(true);
        foreach (var b in towerButtons)
            b.Init(currentBuildPlatform);
    }

    private void OnTowerBuild(TowerBuildPlatform currentBuildPlatform)
    {
        BuildZoneExit();
        currentBuildPlatform.OnTowerBuild -= OnTowerBuild;
    }
}
