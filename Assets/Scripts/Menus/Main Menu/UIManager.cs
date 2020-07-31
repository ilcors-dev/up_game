using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    /**
	 * xy pos of menus:
	 * -mainMenu: x -700
	 * -levelSelector: 1500
	 * offset of 400px from center (x = 400 it the center of the screen view)
	 * */
    public RectTransform mainMenu, skinSelectorMenu, levelSelectorMenu;
    public bool isShopActive = false;
    public static bool isSkinShopActive = false;
    public static bool isLevelShopActive = false;
  
    // Start is called before the first frame update
    void Start()
    {
        //mainMenu.DOMove(new Vector2(400f, 710f), 0.25f);
        mainMenu.DOAnchorPos(new Vector2(400f, 710f), 0.25f);
    }

    public void ShowSkinSelectorMenu()
    {
        isShopActive = true;
        isSkinShopActive = true;
        mainMenu.DOAnchorPos(new Vector2(-700f, 709.8361f), 0.25f);
        skinSelectorMenu.DOAnchorPos(new Vector2(400f, 710f), 0.25f);
    }

    public void ShowLevelSelectorMenu()
    {
        isShopActive = true;
        isLevelShopActive = true;
        mainMenu.DOAnchorPos(new Vector2(-700f, 709.8361f), 0.25f);
        levelSelectorMenu.DOAnchorPos(new Vector2(400f, 710f), 0.25f);
    }

    public void CloseSelectorMenu()
    {
        isShopActive = false;
        if (isLevelShopActive)
        {
            isLevelShopActive = false;
            mainMenu.DOAnchorPos(new Vector2(400f, 709.8361f), 0.25f);
            levelSelectorMenu.DOAnchorPos(new Vector2(1500f, 710f), 0.25f);
        }
        if (isSkinShopActive)
        {
            isSkinShopActive = false;
            mainMenu.DOAnchorPos(new Vector2(400f, 709.8361f), 0.25f);
            skinSelectorMenu.DOAnchorPos(new Vector2(400f, -1000f), 0.25f);
        }
    }

    //public void CloseLevelSelectorMenu()
    //{
    //    isLevelShopActive = false;
    //    mainMenu.DOAnchorPos(new Vector2(400f, 709.8361f), 0.25f);
    //    levelSelectorMenu.DOAnchorPos(new Vector2(1500f, 710f), 0.25f);
    //}
}
