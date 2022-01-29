using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : ABaseUI
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Image offHapticsImage;

    private void Start()
    {
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        coinsText.text = _data.Coins.ToString();
    }

    public void ToggleHaptics()
    {
        Vibration.EnableHaptics = !Vibration.EnableHaptics;
        offHapticsImage.gameObject.SetActive(!Vibration.EnableHaptics);
        Debug.Log(Vibration.EnableHaptics);
        Vibration.Vibrate(50);
    }
}
