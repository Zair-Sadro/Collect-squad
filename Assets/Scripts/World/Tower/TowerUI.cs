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
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private Vector3 posTileCounterBeforeTower;
    [SerializeField] private Vector3 posTileCounterAfterTower;

    [SerializeField] private List<TowerButton> towerButtons = new List<TowerButton>();

    private void OnEnable()
    {
        buildPlatform.OnNotEnoughTiles += OnNotEnoughTiles;
        buildPlatform.OnTowerBuild += OnTowerBuild;
        buildPlatform.OnClearPlatform += OnClearPlatform;
    }


    private void OnDisable()
    {
        buildPlatform.OnNotEnoughTiles -= OnNotEnoughTiles;
        buildPlatform.OnTowerBuild -= OnTowerBuild;
        buildPlatform.OnClearPlatform -= OnClearPlatform;
    }


    private void Start()
    {
        tilesCounter.rectTransform.DOLocalMoveY(posTileCounterBeforeTower.y, 0.2f);
    }
    private void OnClearPlatform()
    {
        tilesCounter.rectTransform.anchoredPosition = posTileCounterBeforeTower;
    }

    private void OnTowerBuild(TowerBuildPlatform platform)
    {
        UpdateHealthAndTimer(platform.ActiveTower);
        tilesCounter.rectTransform.anchoredPosition = posTileCounterAfterTower;
    }

    private void UpdateHealthAndTimer(ATowerObject currentTower)
    {
        if(currentTower.CurrentLevel.LevelType > 0)
        {
            TowerObject tower = (TowerObject)currentTower;
            tower.OnGetDamaged += OnTowerDamaged;
            tower.OnCurrentTowerDestroy += OnTowerDestroy;
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

    public void StartSpawnTimer(float time)
    {
        StartCoroutine(SpawnTimeFill(time));
    }

    private IEnumerator SpawnTimeFill(float time)
    {
        unitSpawnTimer.gameObject.SetActive(true);
        float curTime = 0;

       for (float i = time; 0 <= curTime; i -= Time.deltaTime)
       {
           curTime = i;
           unitSpawnTimer.fillAmount = curTime / time;
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

    public void ButtonsUnsub()
    {
        foreach (var b in towerButtons)
            b.RemoveSubs();
    }

    public void InitButtons(TowerBuildPlatform platform)
    {
        foreach (var b in towerButtons)
            b.Init(platform);
    }

    public void ToogleButtons(bool on)
    {
        buttonsLayout.SetActive(on);
    }
}
