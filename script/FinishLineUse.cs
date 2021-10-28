using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineUse : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        //player arrive finish block, stop counting, destroy this gameobject
        if (collision.transform.tag == "Player")
        {
            //stop counting
            collision.transform.GetComponent<Player>().startFlag = false;

            //change player's finish flag to true
            collision.transform.GetComponent<Player>().finishFlag = true;

            //destroy finishline block
            Destroy(this.gameObject);
        }
    }

}
