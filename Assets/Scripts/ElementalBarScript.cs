using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalBarScript : MonoBehaviour {
    private static MainBattleScript mainBScript;
    private int[] elemPosY = new int[5] { 150, 75, 0, -75, -150 };
    [SerializeField] private GameObject fire, water, heart, armor, ice;
    [SerializeField] private TargetCharacter character;
    private List<GameObject> activeEffectsToShow;
	void Start () {
        activeEffectsToShow = new List<GameObject>();
        mainBScript = GameObject.FindGameObjectWithTag("MainGC").GetComponent<MainBattleScript>();
    }
	void Update () {
        foreach (GameObject gObj in activeEffectsToShow)
            gObj.SetActive(false);
        activeEffectsToShow.Clear();
        if (mainBScript.MagicEffectManager.CheckMagicEffect(character, MagicEffectType.Armor))
            activeEffectsToShow.Add(armor);
        if (mainBScript.MagicEffectManager.CheckMagicEffect(character, MagicEffectType.Fire))
            activeEffectsToShow.Add(fire);
        if (mainBScript.MagicEffectManager.CheckMagicEffect(character, MagicEffectType.Healing))
            activeEffectsToShow.Add(heart);
        if (mainBScript.MagicEffectManager.CheckMagicEffect(character, MagicEffectType.Ice))
            activeEffectsToShow.Add(ice);
        if (mainBScript.MagicEffectManager.CheckMagicEffect(character, MagicEffectType.Water))
            activeEffectsToShow.Add(water);
        for (int i = 0; i < activeEffectsToShow.Count; i++)
            activeEffectsToShow[i].GetComponent<RectTransform>().localPosition = new Vector3(activeEffectsToShow[i].GetComponent<RectTransform>().position.x, elemPosY[i], 0);
        foreach (GameObject gObj in activeEffectsToShow)
            gObj.SetActive(true);
    }
}
