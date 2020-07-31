using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;
    private GameObject playButton;
    private GameObject skinButton;
    private GameObject levelsButton;
    public LevelChanger levelChanger;
    public UIManager uiManager;
    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (uiManager.isShopActive)
            {
                uiManager.CloseSelectorMenu();
            }
            else Application.Quit();
        }
    }
    public void Play()
    {
        levelChanger.FadeToLevel(1);
    }

    //public void SelectLevel()
    //{
    //	mainMenu.SetActive(false);
    //	levelSelector.SetActive(true);
    //}
}
