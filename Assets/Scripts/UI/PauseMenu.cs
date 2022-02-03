using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : ABaseUI
{
    [SerializeField] private Image offHapticsImage;


    public void OnMenuClick()
    {
        _controller.GameController.OnLevelLoad(0);
    }

    public void OnPauseCloseClick()
    {
        _controller.GameController.CurrentState = GameState.Core;
    }

    public void ToggleHaptics()
    {
        Vibration.EnableHaptics = !Vibration.EnableHaptics;
        offHapticsImage.gameObject.SetActive(!Vibration.EnableHaptics);
        Debug.Log(Vibration.EnableHaptics);
        Vibration.Vibrate(50);
    }
}
