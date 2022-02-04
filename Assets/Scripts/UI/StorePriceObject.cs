using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorePriceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Image coinImg;

    public void ToggleCoinImg(bool value)
    {
        coinImg.gameObject.SetActive(value);
    }

    public void SetStatusText(string value)
    {
        statusText.SetText(value);
    }
}
