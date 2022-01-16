using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoreMenu : ABaseUI
{
    [SerializeField] private TMP_Text playerTileCounterText;

    private TileSetter _playerTileSetter;

    public override void Init(UIController Controller, UserData data)
    {
        base.Init(Controller, data);

        if(_controller.GameController.Player != null)
        {
            _playerTileSetter = _controller.GameController.Player.GetComponent<TileSetter>();
            _playerTileSetter.OnTilesCountChanged += PlayerTilesCount;
        }
       
    }

    private void OnDisable()
    {
        if (_controller.GameController.Player != null)
            _playerTileSetter.OnTilesCountChanged -= PlayerTilesCount;
    }

    private void PlayerTilesCount(int value)
    {
        playerTileCounterText.SetText(value.ToString());
    }
}
