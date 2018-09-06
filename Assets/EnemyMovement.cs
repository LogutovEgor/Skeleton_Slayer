using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public bool Move;
    [SerializeField] float Speed;
    private float EnemyCurrPosX;
    //Anim
    private Animator EnemyA;
    void Start () {
        EnemyA = this.GetComponent<Animator>();
        EnemyCurrPosX = this.GetComponent<RectTransform>().position.x; 
        Move = true;
	}
    private void OnEnable()
    {
        Move = true;
    }
    void Update () {
		if(Move)
        {
            EnemyA.SetBool("Move", true);
            this.GetComponent<RectTransform>().position = new Vector3(EnemyCurrPosX, this.GetComponent<RectTransform>().position.y);
            EnemyCurrPosX -= Speed;
        }
        if(EnemyCurrPosX<=0.4f)
        {
            Move = false;
            EnemyA.SetBool("Move", false);
        }
	}
}
