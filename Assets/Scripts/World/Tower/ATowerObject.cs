using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ATowerObject : MonoBehaviour
{
    [SerializeField] protected TowerData data;
    [SerializeField] protected TowerLevelType levelType;

    protected TowerLevel _currentLevel;
    protected TowerBuildPlatform _currentBuildPlatform;

    public TowerData Data => data;
    public TowerLevel CurrentLevel => _currentLevel;


    public virtual void Init(TowerBuildPlatform buildPlatform)
    {
        _currentLevel = data.TowerLevels.Where(t => t.LevelType == levelType).FirstOrDefault();
        _currentBuildPlatform = buildPlatform;
    }


}
