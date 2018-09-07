using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MainBattleScript : MonoBehaviour {

    public static MainBattleScript Instance { get; private set; }

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject buff;
    [SerializeField] private GameObject popUpRandomUpgrade;
    [SerializeField] private GameObject popUpMenu;
    [SerializeField] private GameObject showPopUpMenuButton;
    [SerializeField] private GameObject canvas;


    private GameObject player;
    private MainPlayerScript playerScript;
    private GameObject enemy;
    private MainEnemyScript enemyScript;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private AbilityScriptableClass[] allPlayerAbilities;
    [SerializeField] private AbilityScriptableClass[] playerAbilitiesInGame;
    [SerializeField] private bool playerTurn, enemyTurn;
    [SerializeField] private int turnCount;
    [SerializeField] private int enemyDCount;
    private int strengthCoeff;
    private int totalCoin;
    private int lvlRewardCoin;
    private int skeletonKillsRewardCoin;
    private long exp;
    private int lvl;
    private MagicEffectManagerScript magicEffectManager;
    private void Awake()
    {
        Instance = this;
    }
    public void Start() {


        GetComponent<LocalizationScript>().Start();

        player = Instantiate(playerPrefab);
        playerScript = player.GetComponent<MainPlayerScript>();

        magicEffectManager = GetComponent<MagicEffectManagerScript>();//new MagicEffectManager();
        SelectPlayerAbilities();

        SpawnNewEnemy();
        enemyDCount = 0;
        strengthCoeff = 0;
        turnCount = 0;
        skeletonKillsRewardCoin = 0;
        lvlRewardCoin = 0;
        totalCoin = 0;
        if (GetComponent<SaveScript>().SaveObj == null)
            GetComponent<SaveScript>().Awake();
        lvl = GetComponent<SaveScript>().SaveObj.lvl;
        exp = GetComponent<SaveScript>().SaveObj.exp;
    }
    public void OnEnable()
    {
        playerTurn = true;
        enemyTurn = false;
    }
    public void SelectPlayerAbilities()
    {

        playerAbilitiesInGame = new AbilityScriptableClass[3];
        Actions randomAbility;

        //1 skill const - sword attack
        playerAbilitiesInGame[0] = FindAbility(Actions.PhysicalAttack);
        //2 skill
        do
        {
            randomAbility = (Actions)Random.Range(1, allPlayerAbilities.Length + 1);
            if (randomAbility != playerAbilitiesInGame[0].actionName)
            {
                playerAbilitiesInGame[1] = FindAbility(randomAbility);
                break;
            }
        } while (true);

        //3 skill
        do
        {
            randomAbility = (Actions)Random.Range(1, allPlayerAbilities.Length + 1);

            if ((randomAbility != playerAbilitiesInGame[0].actionName) && (randomAbility != playerAbilitiesInGame[1].actionName))
            {
                playerAbilitiesInGame[2] = FindAbility(randomAbility);
                break;
            }
        } while (true);

        canvas.GetComponent<ButtonControl>().UpdateIcon();
    }
    private AbilityScriptableClass FindAbility(Actions abilityToFind)
    {
        foreach (AbilityScriptableClass A in allPlayerAbilities)
            if (A.actionName == abilityToFind)
                return A;
        return null;
    }

    public void EnemyLogic(Actions prevPlayerAbility)
    {
        float attackChance = 1;
        float armorBuffChance = 1;
        float healingChance = 1;
        float lightningAttackChance = 1;
        float fireAttackChance = 1;
        //float noThing = 0;
        if (enemyScript.HealthPoint <= enemyScript.MaxHealthPoint / 3)
        {
            attackChance += 5;
            armorBuffChance += 30;
            healingChance += 30;
            lightningAttackChance += 10;
            fireAttackChance += 10;
        }
        if ((enemyScript.HealthPoint <= enemyScript.MaxHealthPoint / 2) && (enemyScript.HealthPoint > enemyScript.MaxHealthPoint / 3))
        {
            attackChance += 25;
            armorBuffChance += 25;
            healingChance += 25;
            lightningAttackChance += 15;
            fireAttackChance += 15;
        }
        if (enemyScript.HealthPoint > enemyScript.MaxHealthPoint / 2)
        {
            attackChance += 40;
            armorBuffChance += 5;
            healingChance += 5;
            lightningAttackChance += 30;
            fireAttackChance += 30;
        }


        switch (prevPlayerAbility)
        {
            case Actions.ArmorBuff:
                attackChance /= 2;
                lightningAttackChance *= 1.5f;
                break;
            case Actions.FireAttack:
                healingChance *= 1.2f;
                fireAttackChance *= 1.5f;
                break;
            case Actions.Healing:
                attackChance /= 1.2f;
                armorBuffChance *= 2;
                healingChance *= 2;
                break;
            case Actions.LightningAttack:
                armorBuffChance /= 2;
                healingChance *= 1.5f;
                break;
            case Actions.PhysicalAttack:
                attackChance *= 1.5f;
                armorBuffChance *= 2;
                break;
        }

        if (enemyScript.Armor < 50)
            armorBuffChance /= 10;
        if (playerScript.HealthPoint <= enemyScript.Damage)
        {
            lightningAttackChance *= 2;
            fireAttackChance *= 2;
        }
        enemyScript.ChtoDelat = RandomEvClass.RandomEv(new float[5] { attackChance, armorBuffChance,
            healingChance, lightningAttackChance, fireAttackChance }) + 1;
        if (enemyScript.HealthPoint <= 0)
            enemyScript.ChtoDelat = 0;
    }

    public void SpawnNewEnemy()
    {
        enemy = Instantiate(enemyPrefab);
        enemyScript = enemy.GetComponent<MainEnemyScript>();
    }
    public void Restart()
    {
        Destroy(player);
        Destroy(enemy);
        ///
        magicEffectManager.Clear();
        ///

        player = Instantiate(playerPrefab);
        playerScript = player.GetComponent<MainPlayerScript>();

        SelectPlayerAbilities();
        canvas.GetComponent<ButtonControl>().UpdateIcon();

        SpawnNewEnemy();

        playerTurn = true;
        enemyTurn = false;

        enemyDCount = 0;
        turnCount = 0;
        skeletonKillsRewardCoin = 0;
        lvlRewardCoin = 0;
        totalCoin = 0;

        player.GetComponent<PlayerBackGroundMovement>().Move = true;
        showPopUpMenuButton.SetActive(true);

        GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonControl>().ChangeButtonToUnPressed();
        playerScript.UnBlockButtons();
    }
    public void PlayerAttack(DamageType type)
    {
        enemyScript.TakeDamage(TargetCharacter.Enemy, playerScript.Damage, type);
        GameObject newText = Instantiate(text);
        newText.GetComponent<TextScript>().SetEnemyPos();
        switch (enemyScript.MissAttack)
        {
            case true:
                newText.GetComponent<TextScript>().SetColor(5);
                newText.GetComponent<TextScript>().SetText("MISS");
                break;
            case false:
                newText.GetComponent<TextScript>().SetColor(1);
                newText.GetComponent<TextScript>().SetText("-" + System.Convert.ToString(Mathf.Round(enemyScript.LastDamage)));
                break;
        }
        IncTurnCount();
    }
    public void EnemyAttack(DamageType type)
    {
        playerScript.TakeDamage(TargetCharacter.Player, enemyScript.Damage, type);
        GameObject newText = GameObject.Instantiate(text);
        newText.GetComponent<TextScript>().SetPlayerPos();
        switch (playerScript.MissAttack)
        {
            case true:
                newText.GetComponent<TextScript>().SetColor(5);
                newText.GetComponent<TextScript>().SetText("MISS");
                break;
            case false:
                newText.GetComponent<TextScript>().SetColor(1);
                newText.GetComponent<TextScript>().SetText("-" + System.Convert.ToString(Mathf.Round(playerScript.LastDamage)));
                break;
        }
        IncTurnCount();
    }
    public void OnEnemyDeath()
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonControl>().ChangeButtonToUnPressed();
        playerScript.UnBlockButtons();
        //
        long expOnEDeath = Random.Range(5, 10) + 1 * strengthCoeff;
        exp += expOnEDeath;
        CheckPlayerLvl();
        //
        int CoinOnEDeath = Random.Range(5, 10) + 1 * strengthCoeff;
        skeletonKillsRewardCoin += CoinOnEDeath;
        totalCoin = skeletonKillsRewardCoin + lvlRewardCoin;
        //
        GameObject NewText = Instantiate(text);
        NewText.GetComponent<TextScript>().SetCentralUpperPos();
        NewText.GetComponent<TextScript>().SetColor(3);
        NewText.GetComponent<TextScript>().SetText("+" + System.Convert.ToString(CoinOnEDeath));
        //
        NewText = Instantiate(text);
        NewText.GetComponent<TextScript>().SetCentralPos();
        NewText.GetComponent<TextScript>().SetColor(6);
        NewText.GetComponent<TextScript>().SetText("+" + System.Convert.ToString(expOnEDeath));
        //
        popUpMenu.GetComponent<PopUpMenuScript>().UpdateUI();
        //
        if ((playerScript.HealthPoint <= 0) || (player == null))
            return;
        ///
        if ((enemyDCount >= 5) && (enemyDCount % 5 == 0))
        {
            if (popUpMenu.GetComponent<PopUpMenuScript>().GetPopUpMenuStatus)
                popUpMenu.GetComponent<PopUpMenuScript>().ChangeStatus();
            popUpRandomUpgrade.SetActive(true);
            strengthCoeff++;
            return;
        }
        NextEnemy();
    }
    public void NextEnemy()
    {
        SpawnNewEnemy();
        player.GetComponent<PlayerBackGroundMovement>().Move = true;
        foreach (GameObject E in GameObject.FindGameObjectsWithTag("Hills"))
        {
            E.GetComponent<HillsControl1Script>().Move = true;
        }
        SwitchTurn();
        enemyDCount++;
        //
        GetComponent<SaveScript>().SaveObj.IncSkeletonSumCount();
        //
        magicEffectManager.Clear(TargetCharacter.Enemy);

    }
    public void OnPlayerDeath()
    {
        popUpMenu.GetComponent<PopUpMenuScript>().ChangeStatus();
        popUpMenu.GetComponent<PopUpMenuScript>().ChangeTextTitle("Game Over");
        GameObject.FindGameObjectWithTag("ShowPopUpMenu").SetActive(false);

    }
    public void IncTurnCount()
    {
        turnCount++;
    }
    public void SwitchTurn()
    {
        if (playerTurn)
        {
            playerTurn = false;
            enemyTurn = true;
            return;
        }
        if (enemyTurn)
        {
            enemyTurn = false;
            playerTurn = true;
        }
        turnCount++;
        magicEffectManager.UpdateStatus();
    }
    void CheckPlayerLvl()
    {
        long expToNextLvl = 100 + lvl * 10;
        if (exp >= expToNextLvl)
        {
            lvlRewardCoin += 100 + lvl * 5;
            lvl++;
            long ostatok = -1 * (expToNextLvl - exp);
            exp = ostatok;
        }
    }
    public void ChangeSkeletonKillsRewardCoin(int value)
    {
        skeletonKillsRewardCoin += value;
        totalCoin = skeletonKillsRewardCoin + lvlRewardCoin;
    }
    public void OnApplicationQuit()
    {
        GetComponent<SaveScript>().SaveObj.exp = exp;
        GetComponent<SaveScript>().SaveObj.lvl = lvl;
        GetComponent<SaveScript>().SaveObj.AddCoin(totalCoin);
        GetComponent<SaveScript>().RewriteSave();
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            GetComponent<SaveScript>().SaveObj.exp = exp;
            GetComponent<SaveScript>().SaveObj.lvl = lvl;
            GetComponent<SaveScript>().SaveObj.AddCoin(totalCoin);
            GetComponent<SaveScript>().RewriteSave();
        }
    }
    public GameObject Buff { get { return buff;  } }
    public GameObject Text { get { return text; } }
    public GameObject PopUpMenu { get { return popUpMenu; } }
    public GameObject PopUpRandomUpgrade { get { return popUpRandomUpgrade; } }
    public GameObject Player { get { return player; ; } }
    public GameObject Enemy { get { return enemy; } }
    public AbilityScriptableClass[] PlayerAbilitiesInGame { get { return playerAbilitiesInGame; } }
    public MagicEffectManagerScript MagicEffectManager { get { return magicEffectManager; } }
    public int TurnCount { get { return turnCount; } }
    
    public MainPlayerScript PlayerScript { get { return playerScript; } }
    public MainEnemyScript EnemyScript { get { return enemyScript; } }
    public bool PlayerTurn { get { return playerTurn; } }
    public bool EnemyTurn { get { return enemyTurn; } }
    public int StrengthCoeff { get { return strengthCoeff; } }
    public int TotalCoin { get { return totalCoin; } }
    public long Exp { get { return exp; } }
    public int Lvl { get { return lvl; } }
    public int SkeletonKillsRewardCoin {  get { return skeletonKillsRewardCoin; } }
    public int LvlRewardCoin { get { return lvlRewardCoin; } }
    public int EnemyDCount {  get { return enemyDCount; } }
}
public enum Actions : int
{
    Nothing = 0,
    ArmorBuff,
    FireAttack,
    Healing,
    LightningAttack,
    PhysicalAttack,
    WaterAttack,
    IceAttack
}
public enum DamageType : int
{
    Physical = 0,
    Lightning,
    Fire,
    Water,
    Ice
}
public enum MagicEffectType : int
{
    Armor = 0,
    Healing,
    Fire,
    IncreaseMaxHealthPoint,
    Ice,
    Water
}
public enum TargetCharacter : int
{
    Player = 0,
    Enemy
}

static class RandomEvClass
{
    public static int RandomEv(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
static class CustomConvert
{
    public static string ToCustomShortValue(float value)
    {
        long localValue = Mathf.RoundToInt(value);
        return PrivateToCustomShortValue(localValue);
    }
    public static string ToCustomShortValue(double value)
    {
        value = System.Math.Round(value);
        long localValue = System.Convert.ToInt32(value);
        return PrivateToCustomShortValue(localValue);
    }
    public static string ToCustomShortValue(int value)
    {
        long localValue = Mathf.RoundToInt(value);
        return PrivateToCustomShortValue(localValue);
    }
    private static string PrivateToCustomShortValue(long value)
    {
        if ((value >= 1000) && (value < 1000000))
        {
            value /= 1000;
            return value.ToString() + "k";
        }
        if ((value >= 1000000) && (value < 1000000000))
        {
            value /= 1000000;
            return value.ToString() + "M";
        }
        if ((value >= 1000000000) && (value < 1000000000000))
        {
            value /= 1000000000;
            return value.ToString() + "G";
        }
        return value.ToString();
    }
}

public class MyDebug
{
    public static void Log(string message)
    {
        string filePath = null;
        if ((Application.platform == RuntimePlatform.WindowsEditor))
        {
            filePath = Application.dataPath + "/Log.txt";
        }
        if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.WindowsPlayer))
        {
            filePath = Application.persistentDataPath + "/Log.txt";
        }
            StreamWriter streamWriter = new StreamWriter(filePath, true);
            streamWriter.WriteLine(message);
            streamWriter.Dispose();
            streamWriter.Close();
    }
}