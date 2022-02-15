using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerSwapContent : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private List<TowerSwapButton> swapButtons = new List<TowerSwapButton>();

    private TowerBuildPlatform _tower;
    private TileSetter _playerTileSetter;
    private UserData _data;

    private bool _isSwapTimerSubbed = false;

    public List<TowerSwapButton> SwapButtons => swapButtons;

    public void Init(TowerBuildPlatform tower, TileSetter playerTiles)
    {
        _tower = tower;

        if (_playerTileSetter == null)
            _playerTileSetter = playerTiles;

        if (_data == null)
            _data = GameController.Data;

        InitSwapButtons(tower);
        InitSwapTimer();
    }

    private void InitSwapTimer()
    {
        SwapTimer_OnCanSwap(_tower.SwapTimer.CanSwap);
        SwapTimer_OnTimerChanged(_tower.SwapTimer.CurrentSwapTime);

        if (!_isSwapTimerSubbed)
        {
            _tower.SwapTimer.OnTimerChanged += SwapTimer_OnTimerChanged;
            _tower.SwapTimer.OnCanSwap += SwapTimer_OnCanSwap;
            _isSwapTimerSubbed = true;
        }
    }

    private void SwapTimer_OnCanSwap(bool value)
    {
        ToggleButtons(value);
    }

    private void SwapTimer_OnTimerChanged(float value)
    {
        timerText.text = value <= 0 ? "Ready" : value.ToString("f0");
    }

    public void InitSwapButtons(TowerBuildPlatform tower)
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

    public void ToggleButtons(bool value)
    {
        for (int i = 0; i < swapButtons.Count; i++)
            swapButtons[i].SwapButton.interactable = value;
    }

    public void UnsubSwapTimer()
    {
        if(_tower)
        {
            _tower.SwapTimer.OnTimerChanged -= SwapTimer_OnTimerChanged;
            _tower.SwapTimer.OnCanSwap -= SwapTimer_OnCanSwap;
            _isSwapTimerSubbed = false;
        }
    }
    

}
