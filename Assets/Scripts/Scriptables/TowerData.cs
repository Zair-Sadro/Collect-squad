using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum UnitType
{
    Tower, Sword, Spear, Bow
}

public enum TowerLevelType
{
    None, level1, level2, level3
}

[CreateAssetMenu(menuName ="Data/TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private UnitType type;
    [SerializeField] private List<TowerLevel> towerLevels = new List<TowerLevel>();

    public UnitType Type => type;
    public List<TowerLevel> TowerLevels => towerLevels;


    public TowerLevel GetTowerByLvl(TowerLevelType level)
    {
        return towerLevels.Where(t => t.LevelType == level).FirstOrDefault();
    }
}

[System.Serializable]
public class TowerLevel
{
    [SerializeField] private TowerLevelType levelType;
    [SerializeField] private bool isUpgradeable;
    [SerializeField] private bool isMaxLevel;
    [SerializeField] private int tilesToUpgrade;
    [SerializeField] private BattleUnit unitPrefab;

    public TowerLevelType LevelType => levelType;
    public int TilesToUpgrade => tilesToUpgrade;
    public BattleUnit UnitPrefab => unitPrefab;
    public bool IsMaxLevel => isMaxLevel;
    public bool IsUpgradeable => isUpgradeable;
}