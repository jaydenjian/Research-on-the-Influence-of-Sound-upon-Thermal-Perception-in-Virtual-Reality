using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateCtrl_left : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float lBendingForce;

    // Update is called once per frame
    void Update()
    {
        //get left Bending Force
        lBendingForce = player.GetComponent<Player>().leftBendingForce;         

        //scale the value from 47~70 to 0~0.99(not 0~1 because 1 , anim will overflow)
        lBendingForce = UtilityHelper.Scale(53, 70, 0, 0.99f, lBendingForce);


        //value control anim
        anim.Play("Base Layer.fist_left", 0, lBendingForce);
        
    }   
}
