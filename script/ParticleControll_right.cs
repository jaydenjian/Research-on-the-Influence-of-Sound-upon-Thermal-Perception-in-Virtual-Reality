using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControll_right : MonoBehaviour
{
    private ParticleSystem ps;
    public bool isActive=false;
    public float rBendingForce;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        //reset particle position and add default bias
        this.transform.position = this.GetComponentInParent<Transform>().transform.position + new Vector3(0, -0.083f, 0.06f);
    }

    // Update is called once per frame
    void Update()
    {
        rBendingForce = this.GetComponentInParent<AnimStateCtrl_right>().rBendingForce;
        ps.startLifetime = rBendingForce;

        
       

        if (rBendingForce < 0.1)
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
