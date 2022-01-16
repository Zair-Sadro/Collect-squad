using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombZone : MonoBehaviour
{
    [SerializeField] private TowerBuildPlatform mainTower;

    public TowerBuildPlatform MainTower => mainTower;
}
