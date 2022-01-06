using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTowerButton : MonoBehaviour
{
    [SerializeField] private Button destroyButton;

    private TowerBuildPlatform _buildPlatform;

    public void Init(TowerBuildPlatform buildPlatform)
    {
        _buildPlatform = buildPlatform;
        destroyButton.onClick.AddListener(Destroy);
    }

    private void Destroy()
    {
        if (_buildPlatform == null)
            return;

        _buildPlatform.DestroyTower();
    }

    public void RemoveSubs()
    {
        destroyButton.onClick.RemoveAllListeners();
    }
}
