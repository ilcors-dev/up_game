using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public RectTransform[] levelPreview;
    public RectTransform[] boughtLevels;
    public TextMeshProUGUI levelName;
    public Button leftButton;
    public Button rightButton;
    public Button selectButton;
    public Button buyButton;
    public TextMeshProUGUI totalBalance;
    public TextMeshProUGUI levelCost;
    //private int activePreview;
    //public static int selectedPreview;
    //public GameObject levelSelector;

    //public GameObject mainMenu;
    public UIManager UISliderController;
    public ScrollSnapRect slidingElementsManager;

    //img size width 171.5 -- height 273.3

    public void Init()
    {
        ShowPreviews();
        ReplacePreview();
        //IsSelected();//already called in @SetLevelInfo
        setBalanceText();
        SetLevelInfo(0);
        slidingElementsManager.LerpToPage(Player.activeLevelID);
    }

    public void setBalanceText()
    {
        totalBalance.text = Player.balance.ToString();
    }

    public void SetLevelInfo(int activePage)
    {
        levelName.text = LevelCreator.levels[activePage].levelName;
        CheckIfBought(activePage);
        IsSelected();
    }

    //public void ShowNextTemplate()
    //{
    //    leftButton.interactable = false;
    //    rightButton.interactable = false;
    //    StartCoroutine(InteractableButtons());
    //    float posXToMove = -190f;
    //    if (activePreview != levelPreview.Length - 1)
    //    {
    //        for (int i = 0; i < levelPreview.Length; i++)
    //        {
    //            float currPosX = levelPreview[i].localPosition.x;//levels[i].gameObject.transform.position.x;
    //            levelPreview[i].DOAnchorPos(new Vector2(currPosX - 190f, 10f), 0.10f);
    //            //if (levelPreview[i].localPosition.x == 190f)
    //            //{
    //                //selectedPreview = i;
    //            //}
    //            posXToMove += 190f;
    //        }
    //        activePreview++;
    //        selectedPreview = activePreview;
    //    }
    //    levelName.text = LevelCreator.levels[activePreview].levelName;
    //    IsSelected();
    //    CheckIfBought();
    //    //leftButton.interactable = true;
    //    //CheckSelectedPreview();
    //}


    //public void ShowPreviusTemplate()
    //{
    //    leftButton.interactable = false;
    //    rightButton.interactable = false;
    //    StartCoroutine(InteractableButtons());
    //    float posXToMove = 0f;
    //    if (activePreview != 0)
    //    {
    //        for (int i = 0; i < levelPreview.Length; i++)
    //        {
    //            float currPosX = levelPreview[i].localPosition.x;
    //            levelPreview[i].DOAnchorPos(new Vector2(currPosX + 190f, 10f), 0.10f);
    //            //if (levelPreview[i].localPosition.x == -190f)
    //            //{
    //                //selectedPreview = i;
    //            //}
    //            posXToMove += 190f;
    //        }
    //        activePreview--;
    //        selectedPreview = activePreview;
    //    }

    //    levelName.text = LevelCreator.levels[activePreview].levelName;
    //    IsSelected();
    //    CheckIfBought();
    //    //rightButton.interactable = true;
    //    //CheckSelectedPreview();
    //}

    //private IEnumerator InteractableButtons()
    //{
    //    yield return new WaitForSeconds((levelPreview.Length / 10f));
    //    EnableDisableArrow();

    //}
    ////side stands for left/ride left = 0, right = 1
    //private void EnableDisableArrow(/*int side*/)
    //{
    //    leftButton.interactable = true;
    //    rightButton.interactable = true;
    //}

    public void GoBack()//returns to the main menu
    {
        for (int i = 0; i < levelPreview.Length; i++)
            levelPreview[i].gameObject.SetActive(false);
        UISliderController.CloseSelectorMenu();
    }

    public void ShowPreviews()//shows the previews
    {
        for (int i = 0; i < levelPreview.Length; i++)
            levelPreview[i].gameObject.SetActive(true);
    }

    public void IsSelected()
    {
        if (ScrollSnapRect._currentPage == Player.activeLevelID)
        { 
            selectButton.interactable = false;
        }
        else
        {
            selectButton.interactable = true;
        }
    }

    public void ReplacePreview(/*int levelID*/)
    {
        float m = 0f;//moltiplicatore, elemento [0] è alla pos 0 = 180 * 0,[1] 180*1 
        for (int i = 0; i < levelPreview.Length; i++)
        {
            if (LevelCreator.levels[i].boughtLevel)
            {
                //levelPreview[i].gameObject.SetActive(false);
                //boughtLevels[i].gameObject.SetActive(true);
                //RectTransform tmp = levelPreview[i];
                //levelPreview[i] = boughtLevels[i];
                //levelPreview[i].localPosition = tmp.localPosition;
                levelPreview[i].GetComponent<Image>().overrideSprite = boughtLevels[i].GetComponent<Image>().sprite;
            }
            m += 1.0f;
        }
    }

    public void CheckIfBought(int activePage)
    {
        //for (int i = 0; i < LevelHandler.levels.Count; i++)
        //{
        if (LevelCreator.levels[activePage].boughtLevel == false)
        {
            levelCost.text = "COST: " + LevelCreator.levels[activePage].levelCost.ToString();
            levelCost.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            CanPurchase(activePage);
            //Debug.Log("index : " + LevelHandler.levels[activePreview].levelName);
        }
        else
        {
            //levelPreview[activePreview] = boughtLevel[activePreview];
            levelCost.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            //CanPurchase(activePage);
        }
        //}
    }

    public void CanPurchase(int activePage)
    {
        if (Player.balance < LevelCreator.levels[activePage].levelCost)
            buyButton.interactable = false;
        else if (!buyButton.interactable) buyButton.interactable = true;
    }
}
