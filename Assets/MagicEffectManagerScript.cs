using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectManagerScript : MonoBehaviour {
    [System.Serializable]
    public class MagicEffect
    {
        private int startTurn;
        private int endTurn;
        private bool infinit = false;
        private TargetCharacter targetCharacter;
        private MagicEffectType type;
        float value;
        public MagicEffect(TargetCharacter character, MagicEffectType type, float value, int currentTurn, int lTime, bool infinit)
        {
            startTurn = currentTurn;
            endTurn = startTurn + lTime;
            this.infinit = infinit;
            targetCharacter = character;
            this.type = type;
            switch (type)
            {
                case MagicEffectType.Armor:
                    this.value = value;
                    ChangeArmor((int)value);
                    break;
                case MagicEffectType.Fire:
                    this.value = value / 10;
                    break;
                case MagicEffectType.Healing:
                    this.value = value;
                    break;
                case MagicEffectType.IncreaseMaxHealthPoint:
                    this.value = value;
                    ChangeMaxHealthPoint(value);
                    break;
                case MagicEffectType.Ice:
                    this.value = value * 3;
                    ChangeAgility((int)this.value);
                    break;
                case MagicEffectType.Water:
                    this.value = value * 2;
                    ChangeAgility((int)this.value);
                    break;
            }
        }
        public void OnDestroy()
        {
            switch (type)
            {
                case MagicEffectType.Armor:
                    ChangeArmor((int)-value);
                    break;
                case MagicEffectType.Fire:
                    this.value = value / 10;
                    break;
                case MagicEffectType.Healing:
                    break;
                case MagicEffectType.IncreaseMaxHealthPoint:
                    ChangeMaxHealthPoint(-value);
                    break;
                case MagicEffectType.Ice:
                    ChangeAgility((int)-value);
                    break;
                case MagicEffectType.Water:
                    ChangeAgility((int)-value);
                    break;
            }
        }
        void ChangeMaxHealthPoint(float value)
        {
            switch (targetCharacter)
            {
                case TargetCharacter.Player:
                    //MagicEffectManager.mainBScript.PlayerScript.ChangeMaxHealthPoint(value);
                    MainBattleScript.Instance.PlayerScript.ChangeMaxHealthPoint(value);
                    break;
                case TargetCharacter.Enemy:
                    //MagicEffectManager.mainBScript.EnemyScript.ChangeMaxHealthPoint(value);
                    MainBattleScript.Instance.EnemyScript.ChangeMaxHealthPoint(value);
                    break;
            }
        }
        void ChangeArmor(int value)
        {
            switch (targetCharacter)
            {
                case TargetCharacter.Player:
                    //MagicEffectManager.mainBScript.PlayerScript.ChangeArmor(value);
                    MainBattleScript.Instance.PlayerScript.ChangeArmor(value);
                    break;
                case TargetCharacter.Enemy:
                    //MagicEffectManager.mainBScript.EnemyScript.ChangeArmor(value);
                    MainBattleScript.Instance.EnemyScript.ChangeArmor(value);
                    break;
            }
        }
        void ChangeAgility(int value)
        {
            switch (targetCharacter)
            {
                case TargetCharacter.Player:
                    //MagicEffectManager.mainBScript.PlayerScript.ChangeAgility(value);
                    MainBattleScript.Instance.PlayerScript.ChangeAgility(value);
                    break;
                case TargetCharacter.Enemy:
                    //MagicEffectManager.mainBScript.EnemyScript.ChangeAgility(value);
                    MainBattleScript.Instance.EnemyScript.ChangeAgility(value);
                    break;
            }
        }

        public int EndTurn { get { return endTurn; } }
        public float Value { get { return value; } }
        public bool Infinit { get { return infinit; } }
        public MagicEffectType Type { get { return type; } }
        public TargetCharacter TargetCharacter { get { return targetCharacter; } }
    }
    private MainBattleScript mainBScript; 
    private List<MagicEffect> activeMagicEffects;

    public void Start()
    {
        activeMagicEffects = new List<MagicEffect>();
        mainBScript = GameObject.FindGameObjectWithTag("MainGC").GetComponent<MainBattleScript>();
    }
    public void UpdateStatus()
    {
        FireDamageCalc();
        HealingCalc();
        for (int i = 0; i < activeMagicEffects.Count; i++)
            if ((activeMagicEffects[i].EndTurn <= mainBScript.TurnCount) && (!activeMagicEffects[i].Infinit))
            {
                activeMagicEffects[i].OnDestroy();
                activeMagicEffects.RemoveAt(i);
                i = 0;
            }
    }
    public void AddMagicEffect(TargetCharacter character, MagicEffectType type, float value, int lTime, bool infinit)
    {
        activeMagicEffects.Add(new MagicEffect(character, type, value, mainBScript.TurnCount, lTime, infinit));
    }
    public void Clear()
    {
        activeMagicEffects.Clear();
    }
    public void Clear(TargetCharacter character)
    {
        for (int i = 0; i < activeMagicEffects.Count; i++)
            if (activeMagicEffects[i].TargetCharacter == character)
            {
                activeMagicEffects.RemoveAt(i);
                i = 0;
            }
    }
    public void Clear(TargetCharacter character, MagicEffectType type)
    {
        for (int i = 0; i < activeMagicEffects.Count; i++)
            if ((activeMagicEffects[i].TargetCharacter == character) && (activeMagicEffects[i].Type == type))
            {
                activeMagicEffects.RemoveAt(i);
                i = 0;
            }
    }
    public bool CheckMagicEffect(TargetCharacter character, MagicEffectType effectType)
    {
        foreach (MagicEffect effect in activeMagicEffects)
            if ((effect.TargetCharacter == character) && (effect.Type == effectType))
                return true;
        return false;
    }
    private void FireDamageCalc()
    {
        GameObject NewText;
        float PlayerPassiveDSum = 0;
        float EnemyPassiveDSum = 0;
        foreach (MagicEffect effect in activeMagicEffects)
            if (effect.Type == MagicEffectType.Fire)
                switch (effect.TargetCharacter)
                {
                    case TargetCharacter.Player:
                        PlayerPassiveDSum += effect.Value;
                        break;
                    case TargetCharacter.Enemy:
                        EnemyPassiveDSum += effect.Value;
                        break;
                }
        if (PlayerPassiveDSum > 0)
        {
            NewText = GameObject.Instantiate(mainBScript.Text);
            NewText.GetComponent<TextScript>().SetPlayerUpperPos();
            NewText.GetComponent<TextScript>().SetColor(4);
            NewText.GetComponent<TextScript>().SetText("-" + System.Convert.ToString(Mathf.RoundToInt(PlayerPassiveDSum)));
            ///
            mainBScript.PlayerScript.HealthPoint -= PlayerPassiveDSum;
        }
        if (EnemyPassiveDSum > 0)
        {
            NewText = GameObject.Instantiate(mainBScript.Text);
            NewText.GetComponent<TextScript>().SetEnemyUpperPos();
            NewText.GetComponent<TextScript>().SetColor(4);
            NewText.GetComponent<TextScript>().SetText("-" + System.Convert.ToString(Mathf.RoundToInt(EnemyPassiveDSum)));
            ///
            mainBScript.EnemyScript.HealthPoint -= EnemyPassiveDSum;
        }
    }
    private void HealingCalc()
    {
        GameObject NewText;
        float PlayerHealingNSum = 0;
        float EnemyHealingNDSum = 0;
        foreach (MagicEffect effect in activeMagicEffects)
            if (effect.Type == MagicEffectType.Healing)
                switch (effect.TargetCharacter)
                {
                    case TargetCharacter.Player:
                        PlayerHealingNSum += effect.Value;
                        break;
                    case TargetCharacter.Enemy:
                        EnemyHealingNDSum += effect.Value;
                        break;
                }
        if (PlayerHealingNSum > 0)
        {
            NewText = GameObject.Instantiate(mainBScript.Text);
            NewText.GetComponent<TextScript>().SetPlayerPos();
            NewText.GetComponent<TextScript>().SetColor(1);
            NewText.GetComponent<TextScript>().SetText("+" + System.Convert.ToString(Mathf.RoundToInt(PlayerHealingNSum)));
            ///
            mainBScript.PlayerScript.Healing(PlayerHealingNSum);
        }
        if (EnemyHealingNDSum > 0)
        {
            NewText = GameObject.Instantiate(mainBScript.Text);
            NewText.GetComponent<TextScript>().SetEnemyPos();
            NewText.GetComponent<TextScript>().SetColor(1);
            NewText.GetComponent<TextScript>().SetText("+" + System.Convert.ToString(Mathf.RoundToInt(EnemyHealingNDSum)));
            ///
            mainBScript.EnemyScript.Healing(EnemyHealingNDSum);
        }
    }
}
