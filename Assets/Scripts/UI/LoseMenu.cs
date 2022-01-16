using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : ABaseUI
{
    [SerializeField] private Text sessionCoinsText;

    public void SetSessionScore(int value)
    {
        sessionCoinsText.text = value.ToString();
    }
}
