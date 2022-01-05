using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ATowerObject : MonoBehaviour
{
    [SerializeField] protected TowerData data;
    [SerializeField] protected TowerLevelType levelType;

    protected TowerLevel _currentLevel;

    public TowerData Data => data;
    public TowerLevel CurrentLevel => _currentLevel;


    public virtual void OnEnable()
    {
        Init();
    }

    public virtual void Init()
    {
        Debug.Log(data.name + "is build");
        _currentLevel = data.TowerLevels.Where(t => t.LevelType == levelType).FirstOrDefault();
    }


}
