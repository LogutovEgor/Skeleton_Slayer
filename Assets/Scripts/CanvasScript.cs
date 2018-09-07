using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasScript : MonoBehaviour {
    public static CanvasScript Instance { get; private set; }
    public GameObject PopUpMenuUpgrade;
    public GameObject PopUpMenuDonate;
    public GameObject PopUpInf;
    public GameObject PopUpInventory;
    public GameObject[] MasButton;
    private void Start()
    {
        Instance = this;
    }
}
