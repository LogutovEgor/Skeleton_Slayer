using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateUIElementScript : MonoBehaviour {
    private MainBattleScript battleScript;
    public GameObject Text;
    public bool PlayerHP;
    public bool PlayerArmor;
    public bool PlayerAttack;
    public bool EnemyHP;
    public bool EnemyArmor;
    public bool EnemyAttack;
    public bool EnemyDCount;
    public bool EnemySumD;
    private void Start()
    {
        battleScript = MainBattleScript.Instance;
    }
    void Update () {
		if(PlayerHP)
        {
            float HP = battleScript.PlayerScript.HealthPoint;
            Text.GetComponent<Text>().text = CheckValue(HP);
            return;
        }
        if (PlayerArmor)
        {
            int Armor = battleScript.PlayerScript.Armor;
            Text.GetComponent<Text>().text = CheckValue(Armor);
            return;
        }
        if (PlayerAttack)
        {
            float Damage = battleScript.PlayerScript.Damage;
            Text.GetComponent<Text>().text = CheckValue(Damage);
            return;
        }
        if (EnemyHP)
        { 
            float HP = battleScript.EnemyScript.HealthPoint;
            Text.GetComponent<Text>().text = CheckValue(HP);
            return;
        }
        if (EnemyArmor)
        {
            int Armor = battleScript.EnemyScript.Armor;
            Text.GetComponent<Text>().text = CheckValue(Armor);
            return;
        }
        if (EnemyAttack)
        {
            float Attack = battleScript.EnemyScript.Damage;
            Text.GetComponent<Text>().text = CheckValue(Attack);
            return;
        }
        if (EnemyDCount)
        {
            int EnemyDCount = battleScript.EnemyDCount;
            Text.GetComponent<Text>().text = System.Convert.ToString(EnemyDCount);
            return;
        }
        if (EnemySumD)
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            int EnemySumD = canvas.GetComponent<SaveScript>().SaveObj.skeletonSumCount;
            Text.GetComponent<Text>().text = System.Convert.ToString(EnemySumD);
            return;
        }
    }
    string CheckValue(float value)
    {
        if (value <= 0)
        {
            return "0";
        }
        if ((value > 0) && (value < 0.5f))
        {
            return "≈0";
        }
        if ((value >= 0.5F) && (value < 1))
        {
            return "≈1";
        }
        return Mathf.RoundToInt(value).ToString();
    }

}
