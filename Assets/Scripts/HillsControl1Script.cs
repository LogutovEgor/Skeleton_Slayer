using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillsControl1Script : MonoBehaviour {
    [SerializeField] private GameObject[] HillsMas;
    [SerializeField] private float Speed;
    private float[] StartPos;
    private float[] CurrPos;
    private int temp;
    public bool Move;
	void Start () {
        StartPos = new float[3];
        CurrPos = new float[3];
        for (int i = 0; i < 3; i++)
        {
            StartPos[i] =  HillsMas[i].GetComponent<RectTransform>().position.x;
            CurrPos[i] = StartPos[i];
        }
        temp = 0;
        Move = true;
    }
    private void OnEnable()
    {
        Move = true;
    }
    void Update () {
        if (Move)
        {
            for (int i = 0; i < 3; i++)
            {
                HillsMas[i].GetComponent<RectTransform>().localPosition = new Vector3(CurrPos[i], 0, 0);
                CurrPos[i] -= Speed;
            }
            if ((temp == 0) & (CurrPos[1] <= StartPos[0]))
            {
                CurrPos[0] = StartPos[2];
                temp++;
            }
            if ((temp == 1) & (CurrPos[2] <= StartPos[0]))
            {
                CurrPos[1] = StartPos[2];
                temp++;
            }
            if ((temp == 2) & (CurrPos[0] <= StartPos[0]))
            {
                CurrPos[2] = StartPos[2];
                temp = 0;
            }
        }
    }
}
