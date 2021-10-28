using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;

    public float playerSpeed = 10f;
    //public float playerAltitute;
    //[SerializeField]
    //private float startAltitute;
   

    [Header("ARDUINO")]
    public string arduinoMessage;
    public string[] arduinoMessageArray;

    [Header("BENDING")]
    public float leftBendingForce;
    public float rightBendingForce;

    [Header("HEAT")]
    public float leftHeatingParameter;
    public float rightHeatingParameter;
    [SerializeField]
    private float heatCountingTimer = 0;
    public bool handBalance=false;

    [Header("START")]
    public bool startFlag = false;

    [Header("FINISH")]
    public bool finishFlag=false;

 
    // Update is called once per frame
    void Update()
    {
        
        //read arduino message
        arduinoMessage = GetComponent<ArduinoBasic>().readMessage;

        //split arduinoMessage string to array
        arduinoMessageArray = arduinoMessage.Split(',');

        //assign BendingForce, switch string to float
        rightBendingForce = float.Parse(arduinoMessageArray[0]);
        leftBendingForce = float.Parse(arduinoMessageArray[1]);
        rightHeatingParameter = float.Parse(arduinoMessageArray[2]);
        leftHeatingParameter = float.Parse(arduinoMessageArray[3]);

        CheckHeating();

        //use to start, otherwise player wont move
        if (Input.GetKeyDown(KeyCode.Space)) startFlag = !startFlag;

        //if already finish the game, than stop moving
        if (finishFlag)
        {        
            //cancel gravity
            this.GetComponent<Rigidbody>().useGravity = false;
        }
        else //else keep checking hands whether balance
        {
            //if both hands heat parameter balance, than player move
            if (handBalance)
            {
                //cancel gravity
                this.GetComponent<Rigidbody>().useGravity = false;

                //Player Move
                if(startFlag)  PlayerMove();
            }
            else
            {
                //retrieve gravity
                this.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    //Cheack Heating
    public void CheckHeating()
    {
        // when right and left hand heating Parameter difference less than 10, start counting
        //right and left HeatingParameter > 10, for sure hands is open, not fold
        if (Mathf.Abs(rightHeatingParameter - leftHeatingParameter) < 10 && rightHeatingParameter>10 && leftHeatingParameter>10)
        {
            if (startFlag) //start counting after Press space(start)
            {
                heatCountingTimer += Time.deltaTime;//Time.deltaTime was 1 second
            }
            
            if (heatCountingTimer > 3) //When countinued counting 3 sec, than game finished.
            {
                handBalance = true;
            }            
        }
        else
        {
            
            heatCountingTimer = 0; // if difference more than 10 in any counting time, timer'll reset to 0.
            
            handBalance = false;
        }       
    }

    //Player move
    public void PlayerMove()
    {
        this.transform.position += Vector3.up * playerSpeed * Time.deltaTime;

    }

}
