using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class SaveScript : MonoBehaviour {
    public static SaveScript Instance { get; private set; }
    public ScriptableSaveClass.Save SaveObj;
    string filePath;
    string jsonStr;
    public void Awake () {
        Instance = this;
        if ((Application.platform == RuntimePlatform.WindowsEditor))
        {
            filePath = Application.dataPath + "/Save.json";
        }
        if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.WindowsPlayer))
        {
            filePath = Application.persistentDataPath + "/Save.json";
        }
        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            jsonStr = streamReader.ReadToEnd();
            streamReader.Dispose();
            streamReader.Close();
            SaveObj = new ScriptableSaveClass.Save();
            SaveObj = JsonUtility.FromJson<ScriptableSaveClass.Save>(jsonStr);
            SaveObj.SetValuesWithUpgrade();
            Debug.Log("fileExist");
        }
        else
        {
            SaveObj = new ScriptableSaveClass.Save();
            SaveObj.SetBasicValues();
            SaveObj.SetValuesWithUpgrade();
            jsonStr = JsonUtility.ToJson(SaveObj);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(jsonStr);
            streamWriter.Dispose();
            streamWriter.Close();
        }
            /*if (!PlayerPrefs.HasKey("Save"))
            {
                //scrSaveClass.SaveObj.SetBasicValues();
                save.SetBasicValues();
                //jsonStr = JsonUtility.ToJson(scrSaveClass.SaveObj);
                jsonStr = JsonUtility.ToJson(save);
                PlayerPrefs.SetString("Save", jsonStr);
            }
            else
            {
                jsonStr = PlayerPrefs.GetString("Save");
                //scrSaveClass.SaveObj = JsonUtility.FromJson<ScriptableSaveClass.Save>(jsonStr);
                save = JsonUtility.FromJson<Save>(jsonStr);

            }*/  
    }
    public void RewriteSave()
    {
        if ((Application.platform == RuntimePlatform.WindowsEditor))
        {
            filePath = Application.dataPath + "/Save.json";
        }
        if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.WindowsPlayer))
        {
            filePath = Application.persistentDataPath + "/Save.json";
        }
        if (File.Exists(filePath))
            {
                StreamWriter streamWriter = new StreamWriter(filePath);
                jsonStr = JsonUtility.ToJson(SaveObj);
                streamWriter.Write(jsonStr);
                streamWriter.Dispose();
                streamWriter.Close();
            }
        /*    
        if (PlayerPrefs.HasKey("Save"))
        {
            //jsonStr = JsonUtility.ToJson(scrSaveClass.SaveObj);
            jsonStr = JsonUtility.ToJson(save);
            PlayerPrefs.SetString("Save", jsonStr);
        }*/
    }
}
[CreateAssetMenu(fileName = "Data", menuName = "Save", order = 1)]
public class ScriptableSaveClass : ScriptableObject
{
    public class Save
    {
        public int coin;
        public long exp;
        public int lvl;
        public int skeletonSumCount;
        public float HealthPoint;
        public float maxHealthPoint;
        public float damage;
        public int healingPer;
        public int armorBuffPer;
        public int agility;
        public int armor;
        //
        public List<Weapon> playerWeapons;
        public List<Armor> playerArmor;
        public List<Potion> playerPotions;
        //
        public Weapon activeWeapon;
        public Armor activeBoots, activeCuirass, activeGloves;
        public Potion activePotion;

        [Serializable]
        public class UpgLvlValue
        {
            public string name;
            public int valueLvl;
            public UpgLvlValue(string name, int valueLvl)
            {
                this.name = name;
                this.valueLvl = valueLvl;
            }
        }
        public UpgLvlValue[] upgValues;
        public Save()
        {
            upgValues = new UpgLvlValue[3];
            upgValues[0] = new UpgLvlValue("Health", 0);
            upgValues[1] = new UpgLvlValue("Armor", 0);
            upgValues[2] = new UpgLvlValue("Damage", 0);
        }
        public UpgLvlValue FindUpgValueLvl(string name)
        {
            foreach (UpgLvlValue T in upgValues)
                if (T.name == name)
                    return T;
            return null;
        }
        public void FindUpgValueAndInc(string name)
        {
            foreach (UpgLvlValue T in upgValues)
                if (T.name == name)
                    T.valueLvl++;
        }
        public void AddCoin(int count)
        {
            if (count > 0)
                coin += count;
        }
        public void IncSkeletonSumCount()
        {
            skeletonSumCount++;
        }
        public void SetBasicValues()
        {
            lvl = 0;
            exp = 0;
            coin = 0;
            skeletonSumCount = 0;
            maxHealthPoint = 100;
            damage = 10;
            healingPer = 20;
            armor = 0;
            armorBuffPer = 20;
            agility = 10;
            foreach (UpgLvlValue T in upgValues)
                T.valueLvl = 0;
            //
            playerWeapons = new List<Weapon>();
            playerArmor = new List<Armor>();
            playerPotions = new List<Potion>();
            //
            activeWeapon = null;
            activeBoots = null;
            activeCuirass = null;
            activeGloves = null;
            activePotion =  null;
        }
        public void SetValuesWithUpgrade()
        {
            maxHealthPoint = 100 + 5 * upgValues[0].valueLvl;
            HealthPoint = maxHealthPoint;
            damage = 10 + 5 * upgValues[2].valueLvl;
            armor = 2 * upgValues[1].valueLvl;
            healingPer = 20;
            armorBuffPer = 20;
        }

    }
}
