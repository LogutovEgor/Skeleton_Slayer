using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class TextScript : MonoBehaviour
{
    public Vector3 Position = new Vector3(100,100);
    public GameObject Armor, HP, Coin, Fire, XP;
    public string Char = "";
    void Start()
    {
        this.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        GetComponent<RectTransform>().localPosition = Position;
    }
    private void Update()
    {
        GetComponent<RectTransform>().localPosition = Position;
        Position.y += 0.5f;
    }
    public void SetText(string Text)
    {
        this.GetComponent<Text>().text = Text;
    }
    public void SetPlayerPos()
    {
        Position = new Vector3(-113.9f, 37.1f, 10);
        this.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        Armor.GetComponent<RectTransform>().localPosition = new Vector3(-105, 0, 0);
        HP.GetComponent<RectTransform>().localPosition = new Vector3(-105, 0, 0);
        Fire.GetComponent<RectTransform>().localPosition = new Vector3(-105, 0, 0);
    }
    public void SetPlayerUpperPos()
    {
        Position = new Vector3(-113.9f, 150, 10);
        this.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        Armor.GetComponent<RectTransform>().localPosition = new Vector3(-105, 0, 0);
        HP.GetComponent<RectTransform>().localPosition = new Vector3(-105, 0, 0);
        Fire.GetComponent<RectTransform>().localPosition = new Vector3(-105, 0, 0);
    }
    public void SetEnemyPos()
    {
        Position = new Vector3(113.9f, 37.1f, 10);
        this.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        Armor.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        HP.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        Fire.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
    }
    public void SetEnemyUpperPos()
    {
        Position = new Vector3(113.9f, 150, 10);
        this.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        Armor.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        HP.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        Fire.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
    }
    public void SetCentralUpperPos()
    {
        Position = new Vector3(-100, 250, 10);
        this.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        Armor.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        HP.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        Fire.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        Coin.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
    }
    public void SetCentralPos()
    {
        Position = new Vector3(-100, 150, 10);
        this.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        Armor.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        HP.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        Fire.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        Coin.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
        XP.GetComponent<RectTransform>().localPosition = new Vector3(105, 0, 0);
    }
    public void SetColor(int C)
    {
        switch(C)
        {
            //HP - red
            case 1:
                GetComponent<Text>().color = Color.red;
                HP.SetActive(true);
                break;
            //Armor - grey
            case 2:
                GetComponent<Text>().color = Color.grey;
                Armor.SetActive(true);
                break;
            //Coin - yellow
            case 3:
                GetComponent<Text>().color = Color.yellow;
                Coin.SetActive(true);
                break;
            //Fire - yellow 
            case 4:
                GetComponent<Text>().color = Color.yellow;
                Fire.SetActive(true);
                break;
            //Miss
            case 5:
                GetComponent<Text>().color = Color.grey;
                break;
            //XP
            case 6:
                GetComponent<Text>().color = Color.blue;
                XP.SetActive(true);
                break;
        }
    }
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}