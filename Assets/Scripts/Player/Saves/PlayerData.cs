using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 
 * Class responsible to store the player data, every important player data is here. 
 * 
 */
[System.Serializable]
public class PlayerData
{
    public int bestScore;
    public int balance;
    public int activeSkinID;
    public int activeLevelID;
    public List<int> boughtSkinsID = new List<int>();//the ids of the bought levels
    public List<int> boughtLevelsID = new List<int>();//the ids of the bought levels
    //public PlayerData(Player player)
    //{
    //	bestScore = player.bestScore;
    //	balance = player.balance;
    //	activeLevelID = Player.activeLevelID;
    //}

    public PlayerData(int bestScore, int balance, int activeSkinID, int activeLevelID, List<int> boughtSkinsID, List<int> boughtLevelsID)
    {
        this.bestScore = bestScore;
        this.balance = balance;
        this.activeSkinID = activeSkinID;
        this.activeLevelID = activeLevelID;
        this.boughtSkinsID = boughtSkinsID;
        this.boughtLevelsID = boughtLevelsID;
    }
}
