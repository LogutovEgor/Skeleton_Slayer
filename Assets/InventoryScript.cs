using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {
    public static InventoryScript Instance { get; private set; }
    //
    public enum TabName { WeaponTab, CuirassTab, BootsTab, GlovesTab, PotionTab }
    //
    GameObject canvas;
    CanvasScript canvasScript;
    //
    [SerializeField] Sprite[] knifes, swords, armor, potions;
    //
    List<Weapon> playerWeapons;
    List<Armor> playerCuirasses, playerBoots, playerGloves;
    List<Potion> playerPotions;
    //
    Weapon activeWeapon;
    Armor activeBoots, activeCuirass, activeGloves;
    Potion activePotion;
    //
    [SerializeField] GameObject[] itemCellIcons;
    [SerializeField] GameObject weaponTabIcon, cuirassTabIcon, bootsTabIcon, glovesTabIcon, potionTabIcon;
    [SerializeField] GameObject itemName, itemDescription, itemImage, textCoin, currentPageText;
    [SerializeField] GameObject buttonNext, buttonPrevious, buttonEquip, buttonSell, coins;
    //
    int currentPage;
    int currentTab;
    void Start()
    {
        Instance = this;
        //
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvasScript = canvas.GetComponent<CanvasScript>();
        //
        playerWeapons = new List<Weapon>();
        playerPotions = new List<Potion>();
        //
        activeWeapon = null;
        activeBoots = null;
        activeCuirass = null;
        activeGloves = null;
        activePotion = null;
    }
    void Update () {
		
	}
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        playerWeapons = SaveScript.Instance.SaveObj.playerWeapons;
        playerPotions = SaveScript.Instance.SaveObj.playerPotions;
        //
        playerBoots = new List<Armor>();
        playerCuirasses = new List<Armor>();
        playerGloves = new List<Armor>();
        foreach(Armor armor in SaveScript.Instance.SaveObj.playerArmor)
            switch(armor.ArmorType)
            {
                case ArmorType.Boots:
                    playerBoots.Add(armor);
                    break;
                case ArmorType.Cuirass:
                    playerCuirasses.Add(armor);
                    break;
                case ArmorType.Gloves:
                    playerGloves.Add(armor);
                    break;
            }
        //
        activeWeapon = SaveScript.Instance.SaveObj.activeWeapon;
        if (activeWeapon != null)
            weaponTabIcon.GetComponent<SpriteRenderer>().sprite = activeWeapon.Sprite;

        activeBoots = SaveScript.Instance.SaveObj.activeBoots;
        if (activeBoots != null)
            bootsTabIcon.GetComponent<SpriteRenderer>().sprite = activeBoots.Sprite;

        activeCuirass = SaveScript.Instance.SaveObj.activeCuirass;
        if (activeCuirass != null)
            cuirassTabIcon.GetComponent<SpriteRenderer>().sprite = activeCuirass.Sprite;

        activeGloves = SaveScript.Instance.SaveObj.activeGloves;
        if (activeGloves != null)
            glovesTabIcon.GetComponent<SpriteRenderer>().sprite = activeGloves.Sprite;

        activePotion = SaveScript.Instance.SaveObj.activePotion;
        if (activePotion != null)
            potionTabIcon.GetComponent<SpriteRenderer>().sprite = activePotion.Sprite;
        //
        currentTab = 1;
        currentPage = 1;
        //
        int pages = (playerWeapons.Count % 8 > 0) ? (playerWeapons.Count / 8) + 1 : playerWeapons.Count / 8;
        currentPageText.GetComponent<Text>().text = currentPage.ToString() + "/" + pages.ToString();
        //
    }
    public void SwitchTab(TabName tabName)
    {
        int pages = 0;
        switch(tabName)
        {
            case TabName.WeaponTab:
                pages = (playerWeapons.Count % 8 > 0) ? (playerWeapons.Count / 8) + 1 : playerWeapons.Count / 8;
                break;
            case TabName.CuirassTab:
                pages = (playerCuirasses.Count % 8 > 0) ? (playerCuirasses.Count / 8) + 1 : playerCuirasses.Count / 8;
                break;
            case TabName.BootsTab:
                pages = (playerBoots.Count % 8 > 0) ? (playerBoots.Count / 8) + 1 : playerBoots.Count / 8;
                break;
            case TabName.GlovesTab:
                pages = (playerGloves.Count % 8 > 0) ? (playerGloves.Count / 8) + 1 : playerGloves.Count / 8;
                break;
            case TabName.PotionTab:
                pages = (playerPotions.Count % 8 > 0) ? (playerPotions.Count / 8) + 1 : playerPotions.Count / 8;
                break;
        }
        currentPage = 1;
        currentPageText.GetComponent<Text>().text = currentPage.ToString() + "/" + pages.ToString();

        buttonPrevious.SetActive(false);
        if (pages == 1)
            buttonNext.SetActive(false);

        DrawIcons(tabName, 1);
    }
    public void DrawIcons(TabName tabName, int page)
    {
        int startIndex = --page * 8;
        switch(tabName)
        {
            case TabName.BootsTab:
                for (int i = 0; i < 8; i++, startIndex++)
                {
                    if (startIndex >= playerBoots.Count)
                        break;
                    itemCellIcons[i].GetComponent<SpriteRenderer>().sprite = playerBoots[startIndex].Sprite;
                }
                break;
            case TabName.CuirassTab:
                break;
            case TabName.GlovesTab:
                break;
            case TabName.PotionTab:
                break;
            case TabName.WeaponTab:
                break;
        }
    }
    private void OnDisable()
    {
        foreach (GameObject T in canvasScript.MasButton)
        {
            T.SetActive(true);
        }
        canvas.GetComponent<SaveScript>().RewriteSave();
    }
    public Sprite[] Knifes { get { return knifes; } }
    public Sprite[] Swords { get { return swords; } }
    public Sprite[] Armor { get { return armor; } }
    public Sprite[] Potions { get { return potions; } }
}
public enum Rarity
{
    Common, Uncommon, Rare, Legendary
}
public enum WeaponType
{
    Sword, Knife
}
public enum ArmorType
{
    Boots, Cuirass, Gloves
}
public enum Material
{
    Iron, Silver, Gold
}
public abstract class Item
{
    protected string name;
    protected string description;
    protected int coinPrice;
    protected Rarity rarity;
    protected Sprite sprite;

    public Item(Rarity rarity)
    {
        this.rarity = rarity;
    }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public int CoinPrice { get { return coinPrice; } }
    public Sprite Sprite { get { return sprite; } }
    public Rarity Rarity { get { return rarity; } }
}
public class Weapon : Item
{
    protected int damage;
    protected WeaponType weaponType;
    protected Material material;

    public Weapon(Rarity rarity):base(rarity)
    {
        GenerateWeapon();
        name = rarity.ToString() + " " + material.ToString() + " " + weaponType.ToString();
        description = "+" + damage.ToString() + " DMG";
    }
    protected void GenerateWeapon()
    {
        weaponType = (WeaponType)Random.Range(0,2);
        int minDamage = 0;
        coinPrice = 0;
        switch(rarity)
        {
            case Rarity.Common:
                minDamage = 5;
                material = Material.Iron;
                coinPrice += 5;
                break;
            case Rarity.Uncommon:
                minDamage = 15;
                material = (Material)RandomEvClass.RandomEv(new float[2] { 60f, 40f });
                coinPrice += 15;
                break;
            case Rarity.Rare:
                minDamage = 25;
                material = (Material)(RandomEvClass.RandomEv(new float[2] { 60f, 40f }) + 1);
                coinPrice += 35;
                break;
            case Rarity.Legendary:
                minDamage = 45;
                material = Material.Gold;
                coinPrice += 50;
                break;
        }
        switch(material)
        {
            case Material.Iron:
                minDamage += 5;
                coinPrice += 5;
                break;
            case Material.Silver:
                minDamage += 10;
                coinPrice += 15;
                break;
            case Material.Gold:
                minDamage += 25;
                coinPrice += 25;
                break;
        }
        if (weaponType == WeaponType.Knife)
            minDamage -= 10;

        if (minDamage <= 0)
            minDamage = 1;

        int maxDamage = minDamage + 10;

        damage = Random.Range(minDamage, maxDamage);

        
        int temp = 0;

        if (weaponType == WeaponType.Knife)
        {
            temp = Random.Range(1, 4);
        }
        else if (weaponType == WeaponType.Sword)
        {
            temp = Random.Range(1, 5);
        }
        foreach (Sprite sp in InventoryScript.Instance.Knifes)
            if (sp.name == material.ToString() + "_" + weaponType + "_" + temp.ToString())
                sprite = sp;
    }
    public int Damage { get { return damage; } }
    public WeaponType WeaponType { get { return weaponType; } }
    public Material Material { get { return material; } }
}
public class Armor: Item
{
    protected int armor;
    protected ArmorType armorType;
    protected Material material;

    public Armor(Rarity rarity) : base(rarity)
    {
        GenerateArmor();
        name = rarity.ToString() + " " + material.ToString() + " " + armorType.ToString();
        description = "+" + armor.ToString() + " ARM";
    }
    protected void GenerateArmor()
    {
        armorType = (ArmorType)Random.Range(0, 3);
        int minArmor = 0;
        coinPrice = 0;
        switch (rarity)
        {
            case Rarity.Common:
                minArmor = 1;
                material = Material.Iron;
                coinPrice += 5;
                break;
            case Rarity.Uncommon:
                minArmor = 5;
                material = (Material)RandomEvClass.RandomEv(new float[2] { 60f, 40f });
                coinPrice += 15;
                break;
            case Rarity.Rare:
                minArmor = 10;
                material = (Material)(RandomEvClass.RandomEv(new float[2] { 60f, 40f }) + 1);
                coinPrice += 35;
                break;
            case Rarity.Legendary:
                minArmor = 15;
                material = Material.Gold;
                coinPrice += 50;
                break;
        }
        switch (material)
        {
            case Material.Iron:
                minArmor += 1;
                coinPrice += 5;
                break;
            case Material.Silver:
                minArmor += 5;
                coinPrice += 15;
                break;
            case Material.Gold:
                minArmor += 10;
                coinPrice += 25;
                break;
        }

        if (minArmor <= 0)
            minArmor = 1;

        int maxArmor = minArmor + 5;

        armor = Random.Range(minArmor, maxArmor);


        foreach (Sprite sp in InventoryScript.Instance.Armor)
            if (sp.name == material.ToString() + "_" + armorType)
                sprite = sp;
    }
    public int GetArmor { get { return armor; } }
    public ArmorType ArmorType { get { return armorType; } }
    public Material Material { get { return material; } }
}
public class Potion : Item
{
    protected int healingPer;
    protected string potionSize;

    public Potion(Rarity rarity) : base(rarity)
    {
        GeneratePotion();
        name = rarity.ToString() + " " + potionSize + " healing potion";
        description = "restore " + healingPer.ToString() + "% health";
    }
    protected void GeneratePotion()
    {
        coinPrice = 0;
        switch(RandomEvClass.RandomEv(new float[3] { 40f, 35f, 25f}))
        {
            case 0:
                potionSize = "small";
                healingPer = 20;
                coinPrice += 5;
                break;
            case 1:
                potionSize = "medium";
                healingPer = 40;
                coinPrice += 10;
                break;
            case 2:
                potionSize = "big";
                healingPer = 50;
                coinPrice += 15;
                break;
        }

        switch (rarity)
        {
            case Rarity.Common:
                healingPer += 5;
                coinPrice += 5;
                break;
            case Rarity.Uncommon:
                healingPer += 10;
                coinPrice += 10;
                break;
            case Rarity.Rare:
                healingPer += 15;
                coinPrice += 15;
                break;
            case Rarity.Legendary:
                healingPer += 25;
                coinPrice += 20;
                break;
        }
        foreach (Sprite sp in InventoryScript.Instance.Armor)
            if (sp.name == "Health_Potion_"  + potionSize)
                sprite = sp;
    }

    public int HealingPer { get { return healingPer; } }
    public string PotionSize { get { return potionSize; } }
}

