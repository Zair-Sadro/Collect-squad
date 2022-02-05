using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerSwapContent : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int percentageOfRemainingTiles;
    [Space]
    [SerializeField] private TMP_Text tilesToSwapText;
    [SerializeField] private List<TowerSwapButton> swapButtons = new List<TowerSwapButton>();

    private int _tilesToSwap;
    private TowerBuildPlatform _tower;
    private TileSetter _playerTileSetter;
    private UserData _data;

    public int TilesToSwap
    {
        get
        {
            if (_tower)
            {
                int t = Mathf.RoundToInt((_tower.TilesToUpgrade * percentageOfRemainingTiles) / 100);
                return _tilesToSwap = Mathf.Clamp(t, 0, _data.MaxTiles);
            }

            Debug.LogWarning("Tower is null, cant set tiles to swap");
            return 0;
        }
       
    }

    public void Init(TowerBuildPlatform tower, TileSetter playerTiles)
    {
        _tower = tower;

        if(_playerTileSetter == null)
            _playerTileSetter = playerTiles;

        if (_data == null)
            _data = GameController.Data;
        
        InitSwapButtons(tower);
        UpdateSwapTilesText();
        _tower.OnUpgradeTilesChange += OnTilesChange;
    }

    private void OnDisable()
    {
        _tower.OnUpgradeTilesChange -= OnTilesChange;
    }


    private void InitSwapButtons(TowerBuildPlatform tower)
    {
        for (int i = 0; i < swapButtons.Count; i++)
        {
            swapButtons[i].Init(tower, this, _playerTileSetter);

            if (swapButtons[i].Type == tower.ActiveTower.Data.Type)
                swapButtons[i].gameObject.SetActive(false);
            else
                swapButtons[i].gameObject.SetActive(true);

        }
    }

    private void OnTilesChange(int obj)
    {
        UpdateSwapTilesText();
    }

    private void UpdateSwapTilesText()
    {
        tilesToSwapText.SetText(TilesToSwap.ToString());
    }


}
