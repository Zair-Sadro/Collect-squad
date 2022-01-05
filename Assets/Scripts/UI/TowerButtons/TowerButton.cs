using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private UnitType towerType;
    [SerializeField] private Button buildButton;

    private TowerBuildPlatform _buildPlatform;

    public void Init(TowerBuildPlatform buildPlatform)
    {
        _buildPlatform = buildPlatform;
        buildButton.onClick.AddListener(Build);

    }

    private void Build()
    {
        if(_buildPlatform == null)
        {
            Debug.Log("<color=yellow> Buttons missing building platform </color>");
            return;
        }
        _buildPlatform.BuiltTower(towerType);
    }

    public void RemoveSubs()
    {
        buildButton.onClick.RemoveAllListeners();
    }
}
