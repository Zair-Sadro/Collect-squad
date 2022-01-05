using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    [SerializeField] private TowerData data;
    [SerializeField] private TowerLevelType levelType;
    [SerializeField] private Transform spawnPoint;


    public TowerData Data => data;
    public TowerLevelType LevelType => levelType;

    public void Init()
    {
        Debug.Log(data.name + "is build");
    }

}
