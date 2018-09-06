using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : BasicCharacterScript
{
    private enum PlayerAbilitiesState
    {
        Blocked = 1,
        UnBlocked
    }
    [SerializeField] private PlayerAbilitiesState abilitiesState;
    protected override void Start()
    {
        base.Start();
        currentCharacter = TargetCharacter.Player;

        abilitiesState = PlayerAbilitiesState.UnBlocked;

        healthPoint = save.HealthPoint;
        maxHealthPoint = save.maxHealthPoint;
        damage = save.damage;
        agility = save.agility;
        healingPer= save.healingPer;
        armorBuffPer = save.armorBuffPer;
        armor = save.armor;
    }
    protected override void Update()
    {
        if (healthPoint <= 0)
            StartDestroyAnim();
    }
    public void ChechButtonState(int buttonNum)
    {
        if ((abilitiesState == PlayerAbilitiesState.UnBlocked) && (mainBattleScript.PlayerTurn))
        {
            CheckAbility(buttonNum);
            canvas.GetComponent<ButtonControl>().ChangeButtonToPressed(buttonNum);
            BlockButtons();
        }
    }
    public void CheckAbility(int buttonNum)
    {
        Actions ability = mainBattleScript.PlayerAbilitiesInGame[buttonNum - 1].actionName;
        switch(ability)
        {
            case Actions.ArmorBuff:
                StartMagicEffect(MagicEffectType.Armor);
                break;
            case Actions.FireAttack:
                StartAttack(Actions.FireAttack);
                break;
            case Actions.Healing:
                StartMagicEffect(MagicEffectType.Healing);
                break;
            case Actions.LightningAttack:
                StartAttack(Actions.LightningAttack);
                break;
            case Actions.PhysicalAttack:
                StartAttack(Actions.PhysicalAttack);
                break;
            case Actions.WaterAttack:
                StartAttack(Actions.WaterAttack);
                break;
            case Actions.IceAttack:
                StartAttack(Actions.IceAttack);
                break;
        }
        mainBattleScript.EnemyLogic(ability);
    }
    protected override void EndDestroyAnim()
    {
        base.EndDestroyAnim();
        Destroy(GetComponent<PlayerBackGroundMovement>());
        mainBattleScript.OnPlayerDeath();
    }
    public override void EndMagicEffect(MagicEffectType type)
    {
        base.EndMagicEffect(type);
    }
    public void BlockButtons()
    {
        abilitiesState = PlayerAbilitiesState.Blocked;
    }
    
    public void UnBlockButtons()
    {
        abilitiesState = PlayerAbilitiesState.UnBlocked;
        canvas.GetComponent<ButtonControl>().ChangeButtonToUnPressed();
    }
}
