using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlatformsMain : MonoBehaviour
{
    [SerializeField] private List<TowerBuildPlatform> towerPlatforms = new List<TowerBuildPlatform>();

    public void StopTowerActivity()
    {
        foreach (var tower in towerPlatforms)
            tower.StopTowersActivity();
    }
}
