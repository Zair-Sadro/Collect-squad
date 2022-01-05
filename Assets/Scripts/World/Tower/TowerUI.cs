using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private TowerBuildPlatform buildPlatform;
    [SerializeField] private TMP_Text tilesCounter;
    [SerializeField] private Image unitSpawnTimer;

    public void SetTilesCounter(int current, int needed)
    {
        tilesCounter.text = current + "/" + needed;
    }

    private void OnEnable()
    {
        buildPlatform.OnNotEnoughTiles += OnNotEnoughTiles;
    }

    private void OnDisable()
    {
        buildPlatform.OnNotEnoughTiles -= OnNotEnoughTiles;
    }

    private void OnNotEnoughTiles()
    {
        tilesCounter.transform.DORewind();
        tilesCounter.transform.DOShakeScale(0.2f, 0.3f);
    }
}
