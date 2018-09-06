using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTouchScript : MonoBehaviour {
    protected void EndButtonAnim()
    {
        switch (gameObject.name)
        {
            //Menu
            case "Exit":
                Application.Quit();
                break;
            case "Play":
                SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
                break;
            case "ShowUpgradeMenu":
                CanvasScript.Instance.PopUpMenuUpgrade.SetActive(true);
                foreach (GameObject T in CanvasScript.Instance.MasButton)
                    T.SetActive(false);
                break;
            case "ShowDonateMenu":
                CanvasScript.Instance.PopUpMenuDonate.SetActive(true);
                foreach (GameObject T in CanvasScript.Instance.MasButton)
                    T.SetActive(false);
                break;
            case "ShowInformation":
                CanvasScript.Instance.PopUpInf.SetActive(true);
                foreach (GameObject T in CanvasScript.Instance.MasButton)
                    T.SetActive(false);
                break;
            case "ShowInventory":
                CanvasScript.Instance.PopUpInventory.SetActive(true);
                foreach (GameObject T in CanvasScript.Instance.MasButton)
                    T.SetActive(false);
                break;
            case "CloseUpgradeMenu":
                GameObject.Find("UpgradeMenu").GetComponent<Animator>().SetBool("Close", true);
                break;
            case "UpgradeHealth":
                GameObject.Find("UpgradeMenu").GetComponent<PopUpUpgradeMenuScript>().UpgradeCh("Health");
                break;
            case "UpgradeDamage":
                GameObject.Find("UpgradeMenu").GetComponent<PopUpUpgradeMenuScript>().UpgradeCh("Damage");
                break;
            case "UpgradeArmor":
                GameObject.Find("UpgradeMenu").GetComponent<PopUpUpgradeMenuScript>().UpgradeCh("Armor");
                break;
            case "CloseDonateMenu":
                CanvasScript.Instance.PopUpMenuDonate.GetComponent<Animator>().SetBool("Close", true);
                break;
            case "Donate1":
                CanvasScript.Instance.GetComponent<IAP>().BuyDevSup();
                break;
            case "Donate2":
                CanvasScript.Instance.GetComponent<IAP>().BuyCoins5000();
                break;
            case "CloseInformation":
                CanvasScript.Instance.PopUpInf.GetComponent<Animator>().SetBool("Close", true);
                break;
            case "CloseInventory":
                CanvasScript.Instance.PopUpInventory.GetComponent<Animator>().SetBool("Close", true);
                break;
            //Main
            case "ShowPopUpMenu":
                MainBattleScript.Instance.PopUpMenu.GetComponent<PopUpMenuScript>().ChangeStatus();
                break;
            case "ExitToMenu":
                SaveScript.Instance.SaveObj.AddCoin(MainBattleScript.Instance.TotalCoin);
                SaveScript.Instance.SaveObj.exp = MainBattleScript.Instance.Exp;
                SaveScript.Instance.SaveObj.lvl = MainBattleScript.Instance.Lvl;
                SaveScript.Instance.RewriteSave();
                SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
                break;
            case "Restart":
                MainBattleScript.Instance.PopUpMenu.GetComponent<PopUpMenuScript>().ChangeStatus();
                SaveScript.Instance.SaveObj.AddCoin(MainBattleScript.Instance.TotalCoin);
                SaveScript.Instance.SaveObj.exp = MainBattleScript.Instance.Exp;
                SaveScript.Instance.SaveObj.lvl = MainBattleScript.Instance.Lvl;
                SaveScript.Instance.RewriteSave();
                MainBattleScript.Instance.Restart();
                break;
            case "Upgrade1":
                MainBattleScript.Instance.PopUpRandomUpgrade.GetComponent<PopUpRandomUpgradeScript>().CheckUpgrade(1);
                break;
            case "Upgrade2":
                MainBattleScript.Instance.PopUpRandomUpgrade.GetComponent<PopUpRandomUpgradeScript>().CheckUpgrade(2);
                break;
            case "Upgrade3":
                MainBattleScript.Instance.PopUpRandomUpgrade.GetComponent<PopUpRandomUpgradeScript>().CheckUpgrade(3);
                break;
            case "ButtonA1":
                MainBattleScript.Instance.PlayerScript.ChechButtonState(1);
                break;
            case "ButtonA2":
                MainBattleScript.Instance.PlayerScript.ChechButtonState(2);
                break;
            case "ButtonA3":
                MainBattleScript.Instance.PlayerScript.ChechButtonState(3);
                break;

        }

    }
}
