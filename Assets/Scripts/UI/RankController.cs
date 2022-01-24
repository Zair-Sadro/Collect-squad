using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class RankController : MonoBehaviour
{
    public static RankController Instance;

    [SerializeField] private UserData data;
    [SerializeField] private RankData rankData;
    [Header("UI")]
    [SerializeField] private TMP_Text maxRankText;
    [SerializeField] private TMP_Text currentRankId;
    [SerializeField] private TMP_Text nextRankId;
    [SerializeField] private Image currentRankImage;
    [SerializeField] private Image nextRankImage;
    [SerializeField] private Sprite winGameSkinBar;
    [SerializeField] private Sprite defaultGameSkinBar;

    [SerializeField] private List<Image> gameBarsImg = new List<Image>();

    private Rank _nextRank;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);
    }

    private void Start()
    {
        CheckRank();
    }

    public void CheckRank()
    {
        if (data.Rank == LeagueRank.Rank8)
        {
            //maxRankText.gameObject.SetActive(true);
            for (int i = 0; i < gameBarsImg.Count; i++)
                gameBarsImg[i].gameObject.SetActive(false);

            return;
        }
        SetPlayerRank();
        SetRankIcons();
    }

    private void SetPlayerRank()
    {
        _nextRank = rankData.Ranks.Where(r => r.CurrentRank == data.Rank + 1).FirstOrDefault();
        if (_nextRank == null)
            return;

        var curRank = rankData.Ranks.Where(r => r.CurrentRank == data.Rank).FirstOrDefault();
        currentRankId.SetText(curRank.CurrentId.ToString());
        nextRankId.SetText(_nextRank.CurrentId.ToString());

        if (data.WinsToNextRank >= _nextRank.WinsToOpen)
        {
            data.Rank = _nextRank.CurrentRank;
            data.WinsToNextRank = 0;
        }
    }

    private void SetRankIcons()
    {
        var nextRank = rankData.Ranks.Where(r => r.CurrentRank == data.Rank + 1).FirstOrDefault();


        for (int i = 0; i < rankData.Ranks.Count; i++)
        {
            if (data.Rank == rankData.Ranks[i].CurrentRank)
            {
                currentRankImage.sprite = rankData.Ranks[i].Icon;

                if(data.Rank != LeagueRank.Rank8)
                    nextRankImage.sprite = rankData.Ranks.Where(r => r.CurrentRank == data.Rank + 1)
                                                     .FirstOrDefault().Icon;
            }
        }

        for (int i = 0; i < gameBarsImg.Count; i++)
        {
            if (data.WinsToNextRank == 0)
                gameBarsImg[i].sprite = defaultGameSkinBar;

            for (int j = 0; j < data.WinsToNextRank; j++)
                gameBarsImg[j].sprite = winGameSkinBar;
        }
    }
    
}
