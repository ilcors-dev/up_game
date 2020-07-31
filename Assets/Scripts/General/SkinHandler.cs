using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkinHandler : MonoBehaviour
{
    private int activeSkinID;
    public SkinSelector skinSelector;
    // Start is called before the first frame update
    void Start()
    {
        SetBoughtSkin();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 1)//GAME SCENE
        {
            GameObject player = Instantiate(SkinsCreator.skins[Player.activeSkinID].skinPrefab);
        }
    }
    public void SelectSkin()
    {
        Player.activeSkinID = ScrollSnapRect._currentPage;
        skinSelector.IsSelected();
        SaveSystem.SavePlayer();
    }
    public void SetBoughtSkin()
    {
        for (int i = 0; i < Player.boughtSkinsID.Count; i++)
        {
            if (Player.boughtSkinsID[i] == SkinsCreator.skins[i].skinID)
                SkinsCreator.skins[i].boughtSkin = true;
        }
    }
}
