using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyScript : BasicCharacterScript {
    public int ChtoDelat;
    /*
     * 0 - Nichego
     * 1 - Attack
     * 2 - ArmorBuff
     * 3 - Healing
     * 4 - LightningAttack
     * 5 - FireAttack
     * 6 - IceAttack
     */
    public Actions currentEnemyAction;
    protected override void Start()
    {
        base.Start();
        currentCharacter = TargetCharacter.Enemy;
        RandomizeStats(mainBattleScript.StrengthCoeff);
        ChtoDelat = 0;
        currentEnemyAction = Actions.Nothing;
    }
    protected override void Update()
    {
        if(mainBattleScript.EnemyTurn)
            switch(ChtoDelat)
            {
                case 0:
                    break;
                case 1:
                    StartAttack(Actions.PhysicalAttack);
                    break;
                case 2:
                    StartMagicEffect(MagicEffectType.Armor);
                    break;
                case 3:
                    StartMagicEffect(MagicEffectType.Healing);
                    break;
                case 4:
                    StartAttack(Actions.LightningAttack);
                    break;
                case 5:
                    StartAttack(Actions.FireAttack);
                    break;
                case 6:
                    StartAttack(Actions.IceAttack);
                    break;
                default:
                    break;
            }
        if (healthPoint <= 0)
            StartDestroyAnim();
    }
    public void RandomizeStats(int coeff)
    {
        MainPlayerScript playerScript = mainBattleScript.PlayerScript;
        //
        maxHealthPoint = 25 + Random.Range(1, 10) + 15 * coeff;
        //scale MaxHP with player hp
        //MaxHP += playerScript.HP / 4;
        //
        healthPoint = maxHealthPoint;
        //
        healingPer = 20;
        //
        armor = Random.Range(1, 10) + 1 * coeff;
        //scale Armor with player armor
        //Armor += Mathf.RoundToInt(playerScript.GetArmor/10);
        if (armor > 99)
            armor = 99;
        //
        armorBuffPer = 20;
        //
        damage = 5 + 10 * coeff + Random.Range(1, 5);
        //scale damage with player damage
        //Damage += playerScript.Damage/3;
        //
        Agility = 15 + Random.Range(1, 10);
    }
    protected override void EndAttack(Actions type)
    {
        base.EndAttack(type);
        mainBattleScript.PlayerScript.UnBlockButtons();
    }
    public override void EndMagicEffect(MagicEffectType type)
    {
        base.EndMagicEffect(type);
        mainBattleScript.PlayerScript.UnBlockButtons();
    }
    protected override void EndDestroyAnim()
    {
        base.EndDestroyAnim();
        mainBattleScript.OnEnemyDeath();
    } 
}
