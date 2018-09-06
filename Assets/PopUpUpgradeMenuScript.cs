using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUpgradeMenuScript : MonoBehaviour {
    public GameObject playerCoins;
    public GameObject buttonClose;
    private GameObject canvas;
    private CanvasScript canvasScript;
    public ScriptableSaveClass.Save Save;
    //
    [System.Serializable]
    public class UpgradeButton
    {
        public string name;
        public GameObject UpgIcon;
        public GameObject button;
        public GameObject TextCost;
        public GameObject TextLvlUpgrade;
        public int UpgradeCost;
        public int MaxLvl;
        public bool currSt;
        public void UpdateTextCost()
        {
            TextCost.GetComponent<Text>().text = UpgradeCost.ToString();
        }
    }
    public UpgradeButton[] upgButtonMas;
	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        Save = canvas.GetComponent<SaveScript>().SaveObj;
        canvasScript = canvas.GetComponent<CanvasScript>();
        UpdatePlayerCoins();
        CheckButtonSt();
    }
	void Update () {
        CheckButtonSt();
        //close tyt esli chto to ne rabotaet
    }
    void CheckButtonSt()
    {
        foreach(UpgradeButton B in upgButtonMas)
        {
            if((B.UpgradeCost > Save.coin) || (Save.FindUpgValueLvl(B.name).valueLvl >= B.MaxLvl))
            {
                B.currSt = false;
                B.UpgIcon.GetComponent<Image>().color = Color.red;
            }
            else
            {
                B.currSt = true;
                B.UpgIcon.GetComponent<Image>().color = Color.white;
            }
            switch(B.name)
            {
                case "Health":
                    B.TextLvlUpgrade.GetComponent<Text>().text = Save.FindUpgValueLvl("Health").valueLvl.ToString() + " (+" + 5 * Save.FindUpgValueLvl("Health").valueLvl + ")";
                    break;
                case "Armor":
                    B.TextLvlUpgrade.GetComponent<Text>().text = Save.FindUpgValueLvl("Armor").valueLvl.ToString() + " (+" + 2 * Save.FindUpgValueLvl("Armor").valueLvl + ")";
                    break;
                case "Damage":
                    B.TextLvlUpgrade.GetComponent<Text>().text = Save.FindUpgValueLvl("Damage").valueLvl.ToString() + " (+" + 5 * Save.FindUpgValueLvl("Damage").valueLvl + ")";
                    break;
            }
            if(Save.FindUpgValueLvl(B.name).valueLvl >= B.MaxLvl)
                B.TextLvlUpgrade.GetComponent<Text>().text = "Max" + " (+" + 2 * Save.FindUpgValueLvl(B.name).valueLvl + ")";
            B.UpdateTextCost();
               
        }
    }
    public void UpgradeCh(string buttonName)
    {
        foreach(UpgradeButton B in upgButtonMas)
        {
            if((B.name == buttonName)&&(B.currSt))
            {
                Save.coin -= B.UpgradeCost;
                Save.FindUpgValueAndInc(buttonName);
            }
        }
        UpdatePlayerCoins();
    }
    public void EndButtonAnim()
    {

    }
    public void StartButtonAnim()
    {
    }
    private void OnDisable()
    {
        foreach(GameObject T in canvasScript.MasButton)
        {
            T.SetActive(true);
        }
        canvas.GetComponent<SaveScript>().RewriteSave();
    }
    private void OnApplicationPause(bool pause)
    {
        canvas.GetComponent<SaveScript>().RewriteSave();
    }
    private void OnApplicationQuit()
    {
        canvas.GetComponent<SaveScript>().RewriteSave();
    }
    private void OnEnable()
    {
        UpdatePlayerCoins();
    }
    public void UpdatePlayerCoins()
    {
        if(Save != null)
            playerCoins.GetComponent<Text>().text = Save.coin.ToString();
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
