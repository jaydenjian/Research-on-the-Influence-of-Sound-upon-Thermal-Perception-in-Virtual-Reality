using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControll_left : MonoBehaviour
{
    private ParticleSystem ps;
    public bool isActive = false;
    public float lBendingForce;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        //reset particle position and add default bias
        this.transform.position = this.GetComponentInParent<Transform>().transform.position + new Vector3(-0.13f, -0.083f, 0.18f);
    }

    // Update is called once per frame
    void Update()
    {
        lBendingForce = this.GetComponentInParent<AnimStateCtrl_left>().lBendingForce;
        ps.startLifetime = lBendingForce;




        if (lBendingForce < 0.1)
        {
            ps.Stop();
            isActive = false;
        }
        else
        {
            ps.Play();
            isActive = true;
        }

    }
}
