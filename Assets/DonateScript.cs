using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonateScript : MonoBehaviour {
    GameObject canvas;
    CanvasScript canvasScript;
    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvasScript = canvas.GetComponent<CanvasScript>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        foreach (GameObject T in canvasScript.MasButton)
            T.SetActive(true);
        canvas.GetComponent<SaveScript>().RewriteSave();
    }
}
