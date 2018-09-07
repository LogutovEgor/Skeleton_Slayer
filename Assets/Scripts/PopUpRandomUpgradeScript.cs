using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PopUpRandomUpgradeScript : MonoBehaviour {
    private MainBattleScript battleScript;
    public GameObject ButtonShowPopUpMenu;
    public GameObject[] UI_Icon_Mas;
    public GameObject[] UI_Text_Mas;
    public UpgradeScriptableClass[] allUpgrades;
    public UpgradeScriptableClass[] CurrentUpgrades;
    private void Start()
    {
        battleScript = MainBattleScript.Instance;
    }
    private void OnEnable()
    {
        ButtonShowPopUpMenu.SetActive(false);
        CurrentUpgrades = new UpgradeScriptableClass[3];
      
        string firstUpgrade = "";
        string secondUpgrade = "";
        string thirdUpgrade = "";

        float[] RandomUpgrades = new float[allUpgrades.Length];
        for(int i = 0; i <= RandomUpgrades.Length - 1; i++)
        {
            RandomUpgrades[i] = allUpgrades[i].probability;
        }

        //1 - upgrade
        bool T = false;
        int R = 0;
        do
        {
            R = RandomEvClass.RandomEv(RandomUpgrades);
            firstUpgrade = allUpgrades[R].upgradeName;
            CurrentUpgrades[0] = allUpgrades[R];
            T = true;
            
        } while (!T);
        //2 - upgrade
        T = false;
        do
        {
            R = RandomEvClass.RandomEv(RandomUpgrades);
            if (allUpgrades[R].upgradeName != firstUpgrade)
            {
                secondUpgrade = allUpgrades[R].upgradeName;
                CurrentUpgrades[1] = allUpgrades[R];
                T = true;
            }
        } while (!T);
        //3 - upgrade
        T = false;
        do
        {
            R = RandomEvClass.RandomEv(RandomUpgrades);
            if ((allUpgrades[R].upgradeName != firstUpgrade) && (allUpgrades[R].upgradeName != secondUpgrade))
            {
                thirdUpgrade = allUpgrades[R].upgradeName;
                CurrentUpgrades[2] = allUpgrades[R];
                T = true;
            }
        } while (!T);

        UpdateIconAndText();
    }
    public void UpdateIconAndText()
    {
        for(int i = 0; i <= CurrentUpgrades.Length-1; i++)
        {
            UI_Icon_Mas[i].GetComponent<SpriteRenderer>().sprite = CurrentUpgrades[i].icon;
            UI_Icon_Mas[i].GetComponent<SpriteRenderer>().size = new Vector2(180, 180);
            CurrentUpgrades[i].GenerateValue();
            switch (CurrentUpgrades[i].upgradeName)
            {
                case "Damage":
                    CurrentUpgrades[i].upgradeText = "+" + CurrentUpgrades[i].upgradeValue.ToString() + " " + LocalizationScript.Instance.CurrentLocalization.DMG;
                    break;
                case "Health":
                    CurrentUpgrades[i].upgradeText = "+" + CurrentUpgrades[i].upgradeValue.ToString() + " " + LocalizationScript.Instance.CurrentLocalization.HLT;
                    break;
                case "RandomizeSkills":
                    CurrentUpgrades[i].upgradeText = LocalizationScript.Instance.CurrentLocalization.changeSkills;
                    break;
                case "RestoreHealth":
                    CurrentUpgrades[i].upgradeText = LocalizationScript.Instance.CurrentLocalization.restoreHLT;
                    break;
                case "X2Armor":
                    CurrentUpgrades[i].upgradeText = "X2 " + LocalizationScript.Instance.CurrentLocalization.armor;
                    break;
                case "X2Coin":
                    CurrentUpgrades[i].upgradeText = "X2 " + LocalizationScript.Instance.CurrentLocalization.coins;
                    break;
                case "X2Damage":
                    CurrentUpgrades[i].upgradeText = "X2 " + LocalizationScript.Instance.CurrentLocalization.damage;
                    break;
                case "HealingBuffUpgrade":
                    CurrentUpgrades[i].upgradeText = "+" + CurrentUpgrades[i].upgradeValue.ToString() + " % " + LocalizationScript.Instance.CurrentLocalization.healing;
                    break;
                case "ArmorBuffUpgrade":
                    CurrentUpgrades[i].upgradeText = "+" + CurrentUpgrades[i].upgradeValue.ToString() + " % " + LocalizationScript.Instance.CurrentLocalization.armorBuff;
                    break;
            }
            UI_Text_Mas[i].GetComponent<Text>().text = CurrentUpgrades[i].upgradeText;
        }
    }
    public void CheckUpgrade(int ButtonNum)
    {
        MainPlayerScript MainPS = battleScript.PlayerScript;
        switch(CurrentUpgrades[--ButtonNum].upgradeName)
        {
            case "Damage":
                MainPS.ChangeDamage(CurrentUpgrades[ButtonNum].upgradeValue);
                break;
            case "Health":
                MainPS.ChangeMaxHealthPoint(CurrentUpgrades[ButtonNum].upgradeValue);
                break;
            case "RandomizeSkills":
                battleScript.SelectPlayerAbilities();
                break;
            case "RestoreHealth":
                MainPS.RestoreHealth();
                break;
            case "X2Armor":
                MainPS.ChangeArmor(MainPS.Armor);
                break;
            case "X2Coin":
                battleScript.ChangeSkeletonKillsRewardCoin(battleScript.SkeletonKillsRewardCoin);       
                break;
            case "X2Damage":
                MainPS.ChangeDamage(MainPS.Damage);
                break;
            case "HealingBuffUpgrade":
                MainPS.UpdateHealingPer(CurrentUpgrades[ButtonNum].upgradeValue);
                break;
            case "ArmorBuffUpgrade":
                MainPS.UpdateArmorBuffPer(CurrentUpgrades[ButtonNum].upgradeValue);
                break;
        }
        GetComponent<Animator>().SetBool("Close", true);
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        battleScript.NextEnemy();
        ButtonShowPopUpMenu.SetActive(true);
    }
}
