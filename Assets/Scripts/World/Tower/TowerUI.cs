using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private TowerBuildPlatform buildPlatform;
    [SerializeField] private TMP_Text tilesCounter;
    [SerializeField] private Image unitSpawnTimer;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject healthBar;

    private void OnEnable()
    {
        buildPlatform.OnNotEnoughTiles += OnNotEnoughTiles;
        buildPlatform.OnTowerBuild += OnTowerBuild;
    }


    private void OnDisable()
    {
        buildPlatform.OnNotEnoughTiles -= OnNotEnoughTiles;
        buildPlatform.OnTowerBuild -= OnTowerBuild;
    }

    private void OnTowerBuild(TowerBuildPlatform platform)
    {
        UpdateHealthAndTimer(platform.ActiveTower);
    }

    private void UpdateHealthAndTimer(ATowerObject currentTower)
    {
        if(currentTower.CurrentLevel.LevelType > 0)
        {
            TowerObject tower = (TowerObject)currentTower;
            tower.OnGetDamaged += OnTowerDamaged;
            tower.OnCurrentTowerDestroy += OnTowerDestroy;
            StartCoroutine(SpawnTimeFill(tower.SpawnTime));
        }
    }

    private void OnTowerDestroy(TowerObject tower)
    {
        tower.OnGetDamaged -= OnTowerDamaged;
        tower.OnCurrentTowerDestroy -= OnTowerDestroy;

        unitSpawnTimer.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private void OnTowerDamaged(float cur, float max)
    {
        healthBar.gameObject.SetActive(true);
        healthBarFill.fillAmount = cur / max;
    }

    public void SetTilesCounter(int needed, bool isMaxLevel)
    {
        if(isMaxLevel)
            tilesCounter.gameObject.SetActive(false);
        else
        {
            tilesCounter.gameObject.SetActive(true);
            tilesCounter.text = needed.ToString();
        }

    }

    private IEnumerator SpawnTimeFill(float time)
    {
        unitSpawnTimer.gameObject.SetActive(true);

        for (float i = time; i > 0; i-= Time.deltaTime)
        {
            unitSpawnTimer.fillAmount = i / time;
            if (i <= 0)
                i = time;

            yield return new WaitForEndOfFrame();
        }
    }

    public void ToggleCounter(bool on)
    {
        tilesCounter.gameObject.SetActive(on);
    }


    private void OnNotEnoughTiles()
    {
        tilesCounter.transform.DORewind();
        tilesCounter.transform.DOShakeScale(0.2f, 0.3f);
    }
}
