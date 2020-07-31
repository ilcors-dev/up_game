using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public RectTransform[] skinPreview;
    public RectTransform[] boughtSkin;

    public Button selectButton;
    public Button buyButton;
    public TextMeshProUGUI totalBalance;
    public TextMeshProUGUI levelCost;

    public ScrollSnapRect slidingElementsManager;
    public void Init()
    {
        ShowPreviews();
        ReplacePreview();
        setBalanceText();
        IsSelected();
        slidingElementsManager.LerpToPage(Player.activeSkinID);
    }

    public void setBalanceText()
    {
        totalBalance.text = Player.balance.ToString();
    }

    public void ShowPreviews()//shows the previews
    {
        for (int i = 0; i < skinPreview.Length; i++)
            skinPreview[i].gameObject.SetActive(true);
    }

    public void ReplacePreview()
    {
        for (int i = 0; i < skinPreview.Length; i++)
        {
            if (SkinsCreator.skins[i].boughtSkin)
            {
                //levelPreview[i].gameObject.SetActive(false);
                //boughtLevels[i].gameObject.SetActive(true);
                //RectTransform tmp = levelPreview[i];
                //levelPreview[i] = boughtLevels[i];
                //levelPreview[i].localPosition = tmp.localPosition;
                skinPreview[i].GetComponent<Image>().overrideSprite = boughtSkin[i].GetComponent<Image>().sprite;
            }
        }
    }
    public void IsSelected()
    {
        if (ScrollSnapRect._currentPage == Player.activeSkinID)
        {
            selectButton.interactable = false;
        }
        else
        {
            selectButton.interactable = true;
        }
    }

    public void CheckIfBought(int activePage)
    {
        IsSelected();
        if (SkinsCreator.skins[activePage].boughtSkin == false)
        {
            levelCost.text = "COST: " + LevelCreator.levels[activePage].levelCost.ToString();
            levelCost.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            CanPurchase(activePage);
        }
        else
        {
            levelCost.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            CanPurchase(activePage);
        }
    }
    public void CanPurchase(int activePage)
    {
        if (Player.balance < LevelCreator.levels[activePage].levelCost)
            buyButton.interactable = false;
        else if (!buyButton.interactable) buyButton.interactable = true;
    }
}
