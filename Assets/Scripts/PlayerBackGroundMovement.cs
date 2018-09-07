using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackGroundMovement : MonoBehaviour {
    public bool Move;
    private MainBattleScript MainBS;
    //Anim
    private Animator PlayerA;
    void Start()
    {
        PlayerA = this.GetComponent<Animator>();
        MainBS = GameObject.FindGameObjectWithTag("MainGC").GetComponent<MainBattleScript>();
        Move = true;
    }
    private void OnEnable()
    {
        Move = true;
    }
    void Update()
    {
        if (Move)
        {
            PlayerA.SetBool("Move", true);
            foreach (GameObject E in GameObject.FindGameObjectsWithTag("Hills"))
            {
                E.GetComponent<HillsControl1Script>().Move = true;
            }
        }
        if ((MainBS.Enemy != null) && (!MainBS.Enemy.GetComponent<EnemyMovement>().Move))
        {
            Move = false;
            PlayerA.SetBool("Move", false);
            foreach(GameObject E in GameObject.FindGameObjectsWithTag("Hills"))
            {
                E.GetComponent<HillsControl1Script>().Move = false;
            } 
            
        }
    }
}
