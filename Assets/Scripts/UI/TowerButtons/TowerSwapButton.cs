using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerSwapButton : MonoBehaviour
{
    [SerializeField] private UnitType type;
    [SerializeField] private Button swapButton;

    private TowerBuildPlatform _buildPlatform;
    private TowerSwapContent _swapContent;
    private TileSetter _playerTileSetter;


    public UnitType Type => type;

    public void Init(TowerBuildPlatform tower, TowerSwapContent content, TileSetter tileSetter)
    {
        _buildPlatform = tower;

        if(_swapContent == null)
            _swapContent = content;

        if (_playerTileSetter == null)
            _playerTileSetter = tileSetter;
    }

    private void OnDisable()
    {
        swapButton.onClick.RemoveAllListeners();
    }

    public void SwapTower()
    {
        if(_playerTileSetter.Tiles.Count >= _swapContent.TilesToSwap)
        {
            _playerTileSetter.RemoveTilesAtCount(_swapContent.TilesToSwap);
            _buildPlatform.PlaySwapSound();
            _buildPlatform.BuiltTower(GetSwapTower(type));
        }
    }
    

    private TowerObject GetSwapTower(UnitType type)
    {
        if (_buildPlatform && _buildPlatform.ActiveTower.CurrentLevel.LevelType > 0)
            for (int i = 0; i < _buildPlatform.Towers.Count; i++)
            {
                if (_buildPlatform.Towers[i].Data.Type == type &&
                   _buildPlatform.Towers[i].CurrentLevel.LevelType == _buildPlatform.ActiveTower.CurrentLevel.LevelType)
                    return (TowerObject)_buildPlatform.Towers[i];
            }
           

        return null;
    }
}
