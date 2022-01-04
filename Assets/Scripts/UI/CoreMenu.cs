using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoreMenu : ABaseUI
{
    [SerializeField] private TMP_Text playerTileCounterText;

    private TileSetter _playerTileSetter;

    public override void Init(GameController gameController)
    {
        _controller = gameController;

        _playerTileSetter = _controller.Player.GetComponent<TileSetter>();
        _playerTileSetter.OnTilesCountChanged += PlayerTilesCount;
    }

    private void OnDisable()
    {
        _playerTileSetter.OnTilesCountChanged -= PlayerTilesCount;
    }

    private void PlayerTilesCount(int value)
    {
        playerTileCounterText.SetText(value.ToString());
    }
}
