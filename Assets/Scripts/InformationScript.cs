using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class InformationScript : MonoBehaviour {
    GameObject canvas;
    CanvasScript canvasScript;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvasScript = canvas.GetComponent<CanvasScript>();
    }
    public void OnClick(int num)
    {
        string url = null;
        switch(num)
        {
            case 1:
                url = "http://twitter.com/EderMuniZz";
                break;
            case 2:
                url = "http://twitter.com/WoostarsPixels";
                break;
            case 3:
                url = "https://twitter.com/Jsf23Art";
                break;
            case 4:
                url = "https://www.facebook.com/egor.logutov.984";
                break;
            default:
                return;
        }
        Application.OpenURL(url);
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        foreach (GameObject T in canvasScript.MasButton)
        {
            T.SetActive(true);
        }
        //canvas.GetComponent<SaveScript>().RewriteSave();
    }
}
