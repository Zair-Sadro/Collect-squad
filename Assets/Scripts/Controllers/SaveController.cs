using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;

    [SerializeField] private UserData data;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
            SaveData();

    }

    public void LoadData()
    {
        data.Coins = PlayerPrefs.GetInt("Coins");
        data.Cups = PlayerPrefs.GetInt("Cups");
        data.Rank = (LeagueRank)PlayerPrefs.GetInt("Rank", 0);
        data.WinsToNextRank = PlayerPrefs.GetInt("Wins");
        data.CurrentLevel = PlayerPrefs.GetInt("Level", 1);
        data.MaxTiles = PlayerPrefs.GetInt("Tiles", 5);

        Debug.Log("<color=yellow> Data Loaded.. </color>");

       // foreach (var skin in Instance.data.skins.Skins)
       // {
       //     skin.State = (SkinState)PlayerPrefs.GetInt(skin.SaveString + "_state", 0);
       // }
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt("Coins", Instance.data.Coins);
        PlayerPrefs.SetInt("Cups", Instance.data.Cups);
        PlayerPrefs.SetInt("Rank", (int)Instance.data.Rank);
        PlayerPrefs.SetInt("Wins", Instance.data.WinsToNextRank);
        PlayerPrefs.SetInt("Level", Instance.data.CurrentLevel);
        PlayerPrefs.SetInt("Tiles", Instance.data.MaxTiles);

        Debug.Log("<color=cyan> Data Saved. </color>");

        /// foreach (var skin in Instance.data.skins.Skins)
        /// {
        ///     PlayerPrefs.SetInt(skin.SaveString + "_state", (int)skin.State);
        /// }
    }
}
