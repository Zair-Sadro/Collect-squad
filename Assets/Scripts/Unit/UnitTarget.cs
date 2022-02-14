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
        transform.localPosition = mainTower.transform.localPosition;

        mainTower.OnTowerBuild += OnTowerBuild;
        mainTower.OnClearPlatform += OnClearPlatform;
        mainTower.DestroyByUnits.AddListener(OnClearPlatform);
    }

    private void OnDisable()
    {
        mainTower.OnTowerBuild -= OnTowerBuild;
        mainTower.OnClearPlatform -= OnClearPlatform;
        mainTower.DestroyByUnits.RemoveAllListeners();
    }

    private void OnClearPlatform()
    {
        transform.localPosition = _defaultPos;
    }

    private void OnTowerBuild(TowerBuildPlatform tower)
    {
         transform.localPosition = mainTower.transform.localPosition;
    }

    private void OnDrawGizmos()
    {
        if (mainTower == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, mainTower.transform.position);
    }
}
