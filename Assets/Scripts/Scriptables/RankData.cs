using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeagueRank
{
    Rank1, Rank2, Rank3, Rank4, Rank5, Rank6, Rank7, Rank8
}

[CreateAssetMenu(menuName ="Data/RankData")]
public class RankData : ScriptableObject
{
    public List<Rank> Ranks = new List<Rank>();
}

[System.Serializable]
public class Rank
{
    public LeagueRank CurrentRank;
    public Sprite Icon;
    public int WinsToOpen;
}