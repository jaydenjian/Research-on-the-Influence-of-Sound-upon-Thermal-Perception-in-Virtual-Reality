using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public float rightVolumn;
    public float leftVolumn;

    public float leftParameter;
    public float rightParameter;

    public float volumnAcceleration=0.06f;

    public bool noSound = false;
    public bool staticSound = false;
    public bool gradualSound = false;

    private void Start()
    {
        //get now volumn
        rightVolumn = gameObject.transform.GetChild(0).GetComponent<AudioSource>().volume;
        leftVolumn = gameObject.transform.GetChild(1).GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    void Update()
    {
        //get parameter
        leftParameter = leftHand.GetComponent<AnimStateCtrl_left>().lBendingForce;
        rightParameter = rightHand.GetComponent<AnimStateCtrl_right>().rBendingForce;

        if (noSound)
        {
            NoSound();
        }
        if (staticSound) {
            StaticSound();
        }
        if (gradualSound)
        {
            GradualSound();
        }

        //return new volumn to audio source
        gameObject.transform.GetChild(0).GetComponent<AudioSource>().volume = rightVolumn;
        gameObject.transform.GetChild(1).GetComponent<AudioSource>().volume = leftVolumn;
    }

    void GradualSound()
    {
       
        //increase/decrease volumn with 6% acceleration, 0.06 increase/decrease volumn
        if (rightVolumn < rightParameter) //when now volumn is lower than handParameter, need increase volumn
        {
            rightVolumn += volumnAcceleration * Time.deltaTime;
        }
        else if (rightVolumn > rightParameter)//when now volumn is higher than handParameter, need decrease volumn
        {
            rightVolumn -= volumnAcceleration * Time.deltaTime;
        }

        if (leftVolumn < leftParameter)
        {
            leftVolumn += volumnAcceleration * Time.deltaTime;
        }
        else if (leftVolumn > leftParameter)
        {
            leftVolumn -= volumnAcceleration * Time.deltaTime;
        }

    }

    void NoSound()
    {
        rightVolumn = 0;
        leftVolumn = 0;
    }

    void StaticSound()
    {
        if (rightParameter > 0)
        {
            rightVolumn = 1;
        }
        else
        {
            rightVolumn = 0;
        }
        if (leftParameter>0)
        {
            leftVolumn = 1;
        }
        else
        {
            leftVolumn = 0;
        }
    }
}
