using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonsController : MonoBehaviour
{
    [SerializeField] private TileSetter playerTileSetter;
    [SerializeField] private GameObject buttonsLayout;
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
        buttonsLayout.gameObject.SetActive(false);
        foreach (var b in towerButtons)
            b.RemoveSubs();
    }

    private void BuildZoneEnter(TowerBuildPlatform currentBuildPlatform)
    {
        if (currentBuildPlatform.IsTowerBuild)
            return;

        buttonsLayout.gameObject.SetActive(true);
        foreach (var b in towerButtons)
            b.Init(currentBuildPlatform);
    }
}
