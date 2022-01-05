using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName ="Data/TowerContainerData")]
public class TowerContainerData : ScriptableObject
{
    public List<TowerData> Towers = new List<TowerData>();

    public TowerData GetTowerByType(UnitType type)
    {
        return Towers.Where(t => t.Type == type).FirstOrDefault();
    }
}
