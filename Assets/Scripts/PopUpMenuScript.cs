using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;

public class PopUpMenuScript : MonoBehaviour {
    [SerializeField] GameObject MainGC;
    [SerializeField] Text totalCoinText;
    [SerializeField] Text lvlRewardCoinText;
    [SerializeField] Text skeletonKillsRewardCoinText;
    [SerializeField] Text lvlText;
    [SerializeField] GameObject lvlBarLine;
    [SerializeField] Text expText;
    [SerializeField] Text TextTitle;
    [SerializeField] GameObject[] ButtonMas;
    private bool PopUpMenuStatus = false;
    public void ChangeStatus()
    {
        switch (PopUpMenuStatus)
        {
            case true:
                gameObject.GetComponent<Animator>().SetBool("Close", true);
                break;
            case false:
                gameObject.SetActive(!PopUpMenuStatus);
                foreach (GameObject Obj in ButtonMas)
                    Obj.SetActive(PopUpMenuStatus);
                break;
        }
        PopUpMenuStatus = !PopUpMenuStatus;
    }
    public void ChangeTextTitle(string newText)
    {
        TextTitle.text = newText;
    }
    private void OnEnable()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if ((MainGC != null) && (skeletonKillsRewardCoinText != null))
        {
            int coin = MainGC.GetComponent<MainBattleScript>().SkeletonKillsRewardCoin;
            skeletonKillsRewardCoinText.text = coin.ToString();
        }
        if ((MainGC != null) && (lvlRewardCoinText != null))
        {
            int coin = MainGC.GetComponent<MainBattleScript>().LvlRewardCoin;
            lvlRewardCoinText.text = coin.ToString();
        }
        if ((MainGC != null) && (totalCoinText != null))
        {
            int coin = MainGC.GetComponent<MainBattleScript>().TotalCoin;
            totalCoinText.text = coin.ToString();
        }
        if ((MainGC != null) && (lvlText != null))
        {
            int lvl = MainGC.GetComponent<MainBattleScript>().Lvl;
            lvlText.text = LocalizationScript.Instance.CurrentLocalization.lvl + " " + lvl.ToString();
        }
        if ((MainGC != null) && (expText != null))
        {
            long exp = MainGC.GetComponent<MainBattleScript>().Exp;
            long expToNextLvl = 100 + MainGC.GetComponent<MainBattleScript>().Lvl * 10;
            expText.text = exp.ToString() + "/" + expToNextLvl.ToString() + " " + LocalizationScript.Instance.CurrentLocalization.exp;
            //
            float var1 = (float)expToNextLvl / 100;
            float var2 = (float)exp / var1;
            int var3 = Mathf.RoundToInt(var2);
            //int percent = Mathf.RoundToInt(exp / (expToNextLvl / 100));
            int percent = var3;
            lvlBarLine.GetComponent<SpriteRenderer>().size = new Vector2(630 / 100 * percent, 50);
        }
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
        foreach (GameObject Obj in ButtonMas)
            Obj.SetActive(true);
    }
    public bool GetPopUpMenuStatus
    {
        get { return PopUpMenuStatus; }
    }
    public GameObject[] GetButtonMas
    {
        get { return ButtonMas; }
    }
}

