using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalizationScript : MonoBehaviour {
    public static LocalizationScript Instance { get; private set; }
    [SerializeField] List<Localization> localizations;
    Localization currentLocalization;

    public void Start()
    {
        Instance = this;

        localizations = new List<Localization>();
        localizations.Add(new Localization(SystemLanguage.Russian, "Улучшение", "Здоровье", "Урон", "Броня", "Ур", "ОП", "Донат", "Поддержать разработчика",
            "Купить монеты", "Информация", "Разработчик", "Особая благодарность", "Пауза", "Награда за уровень", "Награда за убийства", "Всего",
            "Выберите одно", "УРН", "ЗДР", "Изменить навыки", "Восстановить ЗДР", "Монет", "Исцеление", "Улучшение брони"));

        localizations.Add(new Localization(SystemLanguage.Ukrainian, "Вдосконалення", "Здоров'я", "Пошкодження", "Броня", "Рівень", "ДО", "Донат", "Підтримати розробника",
            "Купити монет", "Інформація", "Розробник", "Особлива подяка", "Пауза", "Нагорода за рівень", "Винагорода за вбивства", "Загальна кількість",
            "Оберіть одне", "ПШК", "ЗДР", "Змінити навички", "Відновити ЗДР", "Монет", "Зцілення", "Поліпшення броні"));

        localizations.Add(new Localization(SystemLanguage.English, "Upgrade", "Health", "Damage", "Armor", "LVL", "EXP", "Donate", "Support developer",
            "Buy coins", "Information", "Developer", "Special thanks to", "Pause", "LVL reward", "Reward for kills", "Total",
            "Select one", "DMG", "HLT", "Change skills", "Restore HLT", "Coins", "Healing", "Armor buff"));

        currentLocalization = FindLocalization(Application.systemLanguage);

        Text tempText = null;

        if (GameObject.FindGameObjectWithTag("MainGC") == null)
        {
            tempText = FindText("PopUpUpgradeMenuTitleText");
            tempText.GetComponent<Text>().text = currentLocalization.upgrade;
            tempText = FindText("HealthTextName");
            tempText.GetComponent<Text>().text = currentLocalization.health;
            tempText = FindText("DamageTextName");
            tempText.GetComponent<Text>().text = currentLocalization.damage;
            tempText = FindText("ArmorTextName");
            tempText.GetComponent<Text>().text = currentLocalization.armor;
            tempText = FindText("PopUpDonateMenuTitleText");
            tempText.GetComponent<Text>().text = currentLocalization.donate;
            tempText = FindText("Donat1TextName");
            tempText.GetComponent<Text>().text = currentLocalization.supportDeveloper;
            tempText = FindText("Donat2TextName");
            tempText.GetComponent<Text>().text = currentLocalization.buyCoins;
            tempText = FindText("PopUpInfTitleText");
            tempText.GetComponent<Text>().text = currentLocalization.information;
            tempText = FindText("Field_1_Text_1");
            tempText.GetComponent<Text>().text = currentLocalization.developer;
            tempText = FindText("Field_2_Text_1");
            tempText.GetComponent<Text>().text = currentLocalization.specialThanksTo;
        }
        else if (GameObject.FindGameObjectWithTag("MainGC") != null)
        {
            tempText = FindText("PopUpMenuTextTitle");
            tempText.GetComponent<Text>().text = currentLocalization.pause;
            tempText = FindText("LvL_Reward_Text");
            tempText.GetComponent<Text>().text = currentLocalization.lvlReward;
            tempText = FindText("Skeleton_Kills_Reward_Text");
            tempText.GetComponent<Text>().text = currentLocalization.skeletonKillsReward;
            tempText = FindText("Total_Text");
            tempText.GetComponent<Text>().text = currentLocalization.total;
            tempText = FindText("PopUpRandomUpgradeTextTitle");
            tempText.GetComponent<Text>().text = currentLocalization.selectOne;
        }

    }
    private Text FindText(string gameObjectName)
    {
        
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject child in allObjects)
            if (child.name == gameObjectName)
                return child.GetComponent<Text>();
        return null;
    }
    private Localization FindLocalization(SystemLanguage language)
    {
        foreach (Localization localization in localizations)
            if (localization.language == language)
                return localization;
        return FindLocalization(SystemLanguage.English);
    }
    public Localization CurrentLocalization {  get { return currentLocalization; } }

    public class Localization
    {
        public SystemLanguage language; 
        public string upgrade, health, damage, armor, lvl, exp, donate, supportDeveloper, buyCoins, information, developer, specialThanksTo
            , pause, lvlReward, skeletonKillsReward, total, selectOne, DMG, HLT, changeSkills, restoreHLT, coins, healing, armorBuff;
        public Localization(SystemLanguage language, params string[] translate)
        {
            this.language = language;

            upgrade = translate[0];
            health = translate[1];
            damage = translate[2];
            armor = translate[3];
            lvl = translate[4];
            exp = translate[5];
            donate = translate[6];
            supportDeveloper = translate[7];
            buyCoins = translate[8];
            information = translate[9];
            developer = translate[10];
            specialThanksTo = translate[11];
            pause = translate[12];
            lvlReward = translate[13];
            skeletonKillsReward = translate[14];
            total = translate[15];
            selectOne = translate[16];
            DMG = translate[17];
            HLT = translate[18];
            changeSkills = translate[19];
            restoreHLT = translate[20];
            coins = translate[21]; 
            healing = translate[22];
            armorBuff = translate[23];
        }
    }
}
