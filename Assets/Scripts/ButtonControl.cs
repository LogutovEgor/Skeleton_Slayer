using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {
    [SerializeField] private GameObject[] UI_Icon_mas;
    [SerializeField] private Color BlockColor;
    [SerializeField] private Color PressedButtonColor;
    public MainBattleScript MainGC;
    public void ChangeButtonToPressed(int Index)
    {
        foreach(GameObject obj in UI_Icon_mas)
            obj.GetComponent<SpriteRenderer>().color = BlockColor;
        if(Index != 0 )
            UI_Icon_mas[--Index].GetComponent<SpriteRenderer>().color = PressedButtonColor;
    }
    public void ChangeButtonToUnPressed()
    {
        foreach (GameObject obj in UI_Icon_mas)
        {
            obj.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public void UpdateIcon()
    {
        for(int i = 0; i < 3; i++)
        {
            UI_Icon_mas[i].GetComponent<SpriteRenderer>().sprite = MainGC.PlayerAbilitiesInGame[i].icon;
            UI_Icon_mas[i].GetComponent<SpriteRenderer>().size = new Vector2(150, 150);
        }
    }
}
