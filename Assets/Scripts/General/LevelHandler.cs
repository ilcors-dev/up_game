using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UB.Simple2dWeatherEffects.Standard;
public class LevelHandler : MonoBehaviour
{
    const int HILLS = 0;
    const int DESERT = 1;
    const int JUNGLE = 2;
    const int SPACE = 3;
    //PlayerData data;
    private int activeLevelID;
    public LevelSelector levelSelector;
    /**
 * RGB in unity goes from 0f to 1f
 * background color of levels
 * unity default = new Color(49, 77, 121, 1)
 * hills default = new Color(160, 160, 255, 1) --blue
 * space = new Color(0, 0, 0, 1) -- black
 */

    GameObject level = null;

    // Start is called before the first frame update
    void Start()
    {
        SetHandler();
        SetBoughtLevel();
    }
    //CALLED BEFORE EVERYONE
    void SetHandler()
    {
        SetLevelBackground();
    }

    //show the saved level
    private void SetLevelBackground()
    {
        if (level != null)
            Destroy(level);
        if (Player.activeLevelID == HILLS)
            level = Instantiate(LevelCreator.levels[0].levelPrefab);
        //level = Instantiate(levelsPrefab[0]);
        else
            level = Instantiate(LevelCreator.levels[Player.activeLevelID].levelPrefab);
        StormHandler.DisableStorm();
        if (Player.activeLevelID == DESERT || Player.activeLevelID == JUNGLE)//DESERT
        {
            //GameObject t = LevelCreator.levels[Player.activeLevelID].levelPrefab.transform.GetChild(0).gameObject;//first is main background
            //t.transform.localPosition = new Vector3(t.transform.position.x, .18f, t.transform.position.z);
            StormHandler.EnableStorm();
            if (Player.activeLevelID == DESERT)
            {
                StormHandler.storm.Density = 1f;
                StormHandler.storm.VerticalSpeed = 0.0f;
                StormHandler.storm.HorizontalSpeed = 0.2f;
                StormHandler.storm.Color = new Color(249 / 255f, 242 / 255f, 177 / 255f);
            }
            if (Player.activeLevelID == JUNGLE)
            {
                StormHandler.storm.Density =1f;
                StormHandler.storm.VerticalSpeed = 0.5f;
                StormHandler.storm.HorizontalSpeed = 0.2f;
                StormHandler.storm.Color = new Color(213 / 255f, 232 / 255f, 255 / 255f);
            }
        }

        Camera.main.backgroundColor = LevelCreator.levels[Player.activeLevelID].backgroundColor;//level colors start at activeLevel +1 cause index=0 is the unity default bg color		
    }

    public void SelectLevel()
    {
        Player.activeLevelID = ScrollSnapRect._currentPage;
        levelSelector.IsSelected();
        SavePlayer();
        SetHandler();
    }
    public void SetBoughtLevel()
    {
        for (int i = 0; i < Player.boughtLevelsID.Count; i++)
        {
            if (Player.boughtLevelsID[i] == LevelCreator.levels[i].levelID)
                LevelCreator.levels[i].boughtLevel = true;
        }
    }

    private void SavePlayer()
    {
        SaveSystem.SavePlayer();
    }
}
