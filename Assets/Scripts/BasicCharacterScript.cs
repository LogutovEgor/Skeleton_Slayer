using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterScript : MonoBehaviour {
    protected static GameObject mainBattleSys, canvas;
    protected static MainBattleScript mainBattleScript;
    protected static ScriptableSaveClass.Save save;

    protected Animator animator;
    protected GameObject text;
    protected GameObject buff;
    [SerializeField] protected float healthPoint;
    [SerializeField] protected float maxHealthPoint;
    [SerializeField] protected float damage;
    [SerializeField] protected int agility;
    [SerializeField] protected int healingPer;
    [SerializeField] protected int armorBuffPer;
    [SerializeField] protected int armor;
    [SerializeField] protected float lastDamage;
    [SerializeField] protected bool missAttack;
    protected TargetCharacter currentCharacter;
    protected virtual void Start () {
        animator = GetComponent<Animator>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        mainBattleSys = GameObject.FindGameObjectWithTag("MainGC");
        mainBattleScript = mainBattleSys.GetComponent<MainBattleScript>();
        save = mainBattleScript.GetComponent<SaveScript>().SaveObj;
        buff = mainBattleScript.Buff;
        text = mainBattleScript.Text;

        missAttack = false;
    }
	protected virtual void Update () {
		
	}
    public void TakeDamage(TargetCharacter character, float damageFromAnotherChar, DamageType type)
    {
        missAttack = false;
        switch (type)
        {
            case DamageType.Physical:
                if (armor > 0)
                    damageFromAnotherChar -= damageFromAnotherChar / 100 * armor;
                if (armor > 100)
                    damageFromAnotherChar = 0;
                if (mainBattleScript.MagicEffectManager.CheckMagicEffect(currentCharacter, MagicEffectType.Ice))
                    damageFromAnotherChar *= 1.5f;
                //Check miss
                if (RandomEvClass.RandomEv(new float[2] { agility, 100 - agility }) == 0)
                {
                    lastDamage = 0;
                    missAttack = true;
                    return;
                }
                animator.SetBool("Hit", true);
                break;
            case DamageType.Lightning:
                if (armor > 0)
                    damageFromAnotherChar += damageFromAnotherChar * armor / 100;
                if (mainBattleScript.MagicEffectManager.CheckMagicEffect(currentCharacter, MagicEffectType.Ice))
                    damageFromAnotherChar /= 1.25f;
                if (mainBattleScript.MagicEffectManager.CheckMagicEffect(currentCharacter, MagicEffectType.Water))
                    damageFromAnotherChar *= 2;
                animator.SetBool("LightningHit", true);
                break;
            case DamageType.Fire:
                mainBattleScript.MagicEffectManager.Clear(currentCharacter, MagicEffectType.Water);
                ///
                if(mainBattleScript.MagicEffectManager.CheckMagicEffect(currentCharacter, MagicEffectType.Ice))
                {
                    mainBattleScript.MagicEffectManager.Clear(currentCharacter, MagicEffectType.Ice);
                    mainBattleScript.MagicEffectManager.AddMagicEffect(currentCharacter, MagicEffectType.Water, agility, 4, false);
                }
                ///
                mainBattleScript.MagicEffectManager.AddMagicEffect(currentCharacter, MagicEffectType.Fire, damageFromAnotherChar, 4, false);
                ///
                animator.SetBool("FireHit", true);
                break;
            case DamageType.Ice:
                mainBattleScript.MagicEffectManager.Clear(currentCharacter, MagicEffectType.Fire);
                mainBattleScript.MagicEffectManager.Clear(currentCharacter, MagicEffectType.Healing);

                if (mainBattleScript.MagicEffectManager.CheckMagicEffect(currentCharacter, MagicEffectType.Water))
                    damageFromAnotherChar *= 1.25f;
                mainBattleScript.MagicEffectManager.AddMagicEffect(currentCharacter, MagicEffectType.Ice, agility, 4, false);
                mainBattleScript.MagicEffectManager.Clear(currentCharacter, MagicEffectType.Water);
                ///
                animator.SetBool("IceHit", true);
                break;
            case DamageType.Water:
                mainBattleScript.MagicEffectManager.Clear(currentCharacter, MagicEffectType.Fire);
                damageFromAnotherChar /= 10;
                mainBattleScript.MagicEffectManager.AddMagicEffect(currentCharacter, MagicEffectType.Water, agility, 4, false);
                ///
                animator.SetBool("WaterHit", true);
                break;
        }
        healthPoint -= damageFromAnotherChar;
        lastDamage = damageFromAnotherChar;
    }

    public void EndTakeDamage(DamageType type)
    {
        switch (type)
        {
            case DamageType.Physical:
                animator.SetBool("Hit", false);
                break;
            case DamageType.Lightning:
                animator.SetBool("LightningHit", false);
                break;
            case DamageType.Fire:
                animator.SetBool("FireHit", false);
                break;
            case DamageType.Ice:
                animator.SetBool("IceHit", false);
                break;
            case DamageType.Water:
                animator.SetBool("WaterHit", false);
                break;
        }
    }

    public void StartDestroyAnim()
    {
        animator.SetBool("Destroy", true);
    }

    protected virtual void EndDestroyAnim()
    {
        Destroy(gameObject);
    }

    public void StartAttack(Actions ability)
    {
        switch (ability)
        {
            case Actions.PhysicalAttack:
                animator.SetBool("Attack", true);
                break;
            case Actions.LightningAttack:
                animator.SetBool("LightningCast", true);
                break;
            case Actions.FireAttack:
                animator.SetBool("FireCast", true);
                break;
            case Actions.WaterAttack:
                animator.SetBool("WaterCast", true);
                break;
            case Actions.IceAttack:
                animator.SetBool("IceCast", true);
                break;
        }
    }

    protected virtual void EndAttack(Actions ability)
    {
        switch (ability)
        {
            case Actions.PhysicalAttack:
                animator.SetBool("Attack", false);
                break;
            case Actions.LightningAttack:
                animator.SetBool("LightningCast", false);
                break;
            case Actions.FireAttack:
                animator.SetBool("FireCast", false);
                break;
            case Actions.WaterAttack:
                animator.SetBool("WaterCast", false);
                break;
            case Actions.IceAttack:
                animator.SetBool("IceCast", false);
                break;
        }
        mainBattleScript.SwitchTurn();
    }

    protected void DamageEnemy(DamageType type)
    {
        switch (currentCharacter)
        {
            case TargetCharacter.Enemy:
                mainBattleScript.EnemyAttack(type);
                break;
            case TargetCharacter.Player:
                mainBattleScript.PlayerAttack(type);
                break;
        }
    }

    public void StartMagicEffect(MagicEffectType type)
    {
        switch (type)
        {
            case MagicEffectType.Armor:
                animator.SetBool("Def", true);
                break;
            case MagicEffectType.Healing:
                animator.SetBool("Healing", true);
                break;
        }
    }
    protected void MidMagicEffect(MagicEffectType type)
    {
        GameObject newText = Instantiate(text);
        if (currentCharacter == TargetCharacter.Player)
            newText.GetComponent<TextScript>().SetPlayerPos();
        else if (currentCharacter == TargetCharacter.Enemy)
            newText.GetComponent<TextScript>().SetEnemyPos();
        switch (type)
        {
            case MagicEffectType.Armor:
                int armorMagicEffectNum = (int)((float)armor / 100f * armorBuffPer);
                ///
                mainBattleScript.MagicEffectManager.AddMagicEffect(currentCharacter, type, armorMagicEffectNum, 2, false);
                ///
                newText.GetComponent<TextScript>().SetColor(2);
                newText.GetComponent<TextScript>().SetText("+" + System.Convert.ToString(armorMagicEffectNum));
                break;
            case MagicEffectType.Healing:
                float healingMagicEffectNum = maxHealthPoint / 100 * healingPer;
                ///
                Healing(healingMagicEffectNum);
                ///
                mainBattleScript.MagicEffectManager.AddMagicEffect(currentCharacter, type, healingMagicEffectNum, 4, false);
                ///
                newText.GetComponent<TextScript>().SetColor(1);
                newText.GetComponent<TextScript>().SetText("+" + System.Convert.ToString(healingMagicEffectNum));
                break;
        }
    }
    public virtual void EndMagicEffect(MagicEffectType type)
    {
        switch (type)
        {
            case MagicEffectType.Armor:
                animator.SetBool("Def", false);
                break;
            case MagicEffectType.Healing:
                animator.SetBool("Healing", false);
                break;
        }
        mainBattleScript.SwitchTurn();
    }
    public void ChangeMaxHealthPoint(float value)
    {
        maxHealthPoint += value;
        healthPoint = (healthPoint > maxHealthPoint) ? maxHealthPoint : healthPoint;
    }
    public void Healing(float value)
    {
        healthPoint += value;
        if (healthPoint > maxHealthPoint)
            healthPoint = maxHealthPoint;
    }
    public void RestoreHealth()
    {
        healthPoint = maxHealthPoint;
    }
    public void ChangeArmor(int value)
    {
        armor += value;
    }
    public void ChangeDamage(float value)
    {
        Damage += value;
        Damage = (Damage < 0) ? 0 : Damage;
    }
    public void UpdateArmorBuffPer(int value)
    {
        armorBuffPer += value;
    }
    public void UpdateHealingPer(int value)
    {
        healingPer += value;
    }
    public void ChangeAgility(int value)
    {
        agility += value;
        if (agility > 100)
            agility = 100;
    }
    public float HealthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; }
    }
    public float MaxHealthPoint
    {
        get { return maxHealthPoint; }
        set { maxHealthPoint = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }
    public int HealingPer
    {
        get { return healingPer; }
        set { healingPer = value; }
    }
    public int ArmorBuffPer
    {
        get { return armorBuffPer; }
        set { armorBuffPer = value; }
    }
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    public float LastDamage
    {
        get { return lastDamage; }
        set { lastDamage = value; }
    }
    public bool MissAttack
    {
        get { return missAttack; }
        set { missAttack = value; }
    }
}
