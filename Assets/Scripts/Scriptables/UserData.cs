using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    public LeagueRank Rank;
    public int Coins;
    public int Cups;
    public int MaxTiles;
    public int WinsToNextRank;
    public int CurrentLevel;

    [ContextMenu("Clear Data")]
    private void ClearData()
    {
        Rank = LeagueRank.Rank1;
        Coins = 0;
        Cups = 0;
        MaxTiles = 5;
        WinsToNextRank = 0;
        CurrentLevel = 0;
        PlayerPrefs.DeleteAll();
    }
}
