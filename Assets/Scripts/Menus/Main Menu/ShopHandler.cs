using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopHandler : MonoBehaviour
{
    public SkinSelector skinSelector;
    public LevelSelector levelSelector;
    
    private PlayerData data;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            //data = SaveSystem.LoadPlayer();
            //activeLevelID = data.activeLevelID;
            //Player.activeLevelID = activeLevelID;
            //Player.balance = data.balance;
            //Player.bestScore = data.bestScore;
            //Player.boughtLevelID = data.boughtLevelID;
            SaveSystem.LoadAndAssign();
        }
        catch (Exception e)//no saves yet, it will set the default level preventing exception
        {
            Player.activeSkinID = 0;//0 = DEFAULT SKIN
            Player.activeLevelID = 0;//0 = HILLS
            Player.boughtSkinsID.Add(SkinsCreator.skins[0].skinID);//hills DEFAULT
            Player.boughtLevelsID.Add(LevelCreator.levels[0].levelID);//hills DEFAULT

            SaveSystem.SavePlayer();
        }
        //UpdateBalance();
    }

    public void BuyLevel()
    {
        Levels targetLevel = LevelCreator.levels[ScrollSnapRect._currentPage];
        if (Player.balance >= targetLevel.levelCost)//if player has enough money to buy the level
        {
            Player.balance -= targetLevel.levelCost;
            targetLevel.boughtLevel = true;
            Player.boughtLevelsID.Add(targetLevel.levelID);
            //for (int i = 0; i < Player.boughtLevelID.Count; i++)
            //{
            //    Debug.Log("level id -> " + Player.boughtLevelID[i]);
            //}
            levelSelector.ReplacePreview(/*targetLevel.levelID*/);
            levelSelector.setBalanceText();
            levelSelector.CheckIfBought(ScrollSnapRect._currentPage);
            SaveSystem.SavePlayer();
            SaveSystem.LoadAndAssign();
        }
    }
    public void BuySkin()
    {
        Skins targetLevel = SkinsCreator.skins[ScrollSnapRect._currentPage];
        if (Player.balance >= targetLevel.skinCost)//if player has enough money to buy the level
        {
            Player.balance -= targetLevel.skinCost;
            targetLevel.boughtSkin = true;
            Player.boughtSkinsID.Add(targetLevel.skinID);
            skinSelector.ReplacePreview();
            skinSelector.setBalanceText();
            skinSelector.CheckIfBought(ScrollSnapRect._currentPage);
            SaveSystem.SavePlayer();
            SaveSystem.LoadAndAssign();
        }
    }
    public void UpdateBalance()
    {
        data = SaveSystem.LoadPlayer();
        //coinBalance.text = Player.balance.ToString();
    }
}
