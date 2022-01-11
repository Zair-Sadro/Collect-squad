using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTarget : MonoBehaviour
{
    [SerializeField] private TowerBuildPlatform mainTower;

    private Vector3 _defaultPos;

    private void Awake()
    {
        _defaultPos = transform.localPosition;
    }

    private void Start()
    {
        mainTower.OnTowerBuild += OnTowerBuild;
        mainTower.OnClearPlatform += OnClearPlatform;
    }

    private void OnDisable()
    {
        mainTower.OnTowerBuild -= OnTowerBuild;
        mainTower.OnClearPlatform -= OnClearPlatform;
    }

    private void OnClearPlatform()
    {
        transform.localPosition = _defaultPos;
    }

    private void OnTowerBuild(TowerBuildPlatform tower)
    {
        if (tower.ActiveTower.CurrentLevel.LevelType > 0)
            transform.localPosition = tower.transform.localPosition;
    }
}
