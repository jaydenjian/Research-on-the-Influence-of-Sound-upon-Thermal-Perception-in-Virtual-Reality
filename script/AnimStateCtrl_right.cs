using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateCtrl_right : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float rBendingForce;

    // Update is called once per frame
    void Update()
    {

        //get right Bending Force
        rBendingForce = player.GetComponent<Player>().rightBendingForce;   

        //scale the value from 41~71 to 0~0.99(not 0~1 because 1 , anim will overflow)
        rBendingForce = UtilityHelper.Scale(41, 71, 0, 0.99f, rBendingForce);


        //value control anim
        anim.Play("Base Layer.fist_right", 0, rBendingForce);

    }
}
