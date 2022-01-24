using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : ABaseUI
{
    [SerializeField] private TMP_Text coinsText;

    private void Start()
    {
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        coinsText.text = _data.Coins.ToString();
    }

}
