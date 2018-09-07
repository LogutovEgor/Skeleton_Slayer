using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade", order = 1)]
public class UpgradeScriptableClass : ScriptableObject {
    public Sprite icon;
    public string upgradeName;
    public string upgradeText;
    public bool randomValue;
    public int min;
    public int max;
    public int upgradeValue;
    public float probability;
    public void GenerateValue()
    {
        if (randomValue)
            this.upgradeValue = Random.Range(min, max);
    }
}