using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCtrl : MonoBehaviour
{
    public float timer;
    public bool startFlag = false;
    public GameObject player;
    public GameObject FinishCanvas;

    void Update()
    {
        startFlag = player.GetComponent<Player>().startFlag;

        //when button press, start counting
        if (startFlag)
        {
            timer += Time.deltaTime;

        }

        //player arrive finish line, finishbox appear
        if (player.GetComponent<Player>().finishFlag==true && timer > 0)
        {
            
            FinishCanvas.SetActive(true);
        }

        //display time on ctrl screen
        this.GetComponentInChildren<Text>().text = timer.ToString("0.00");

        //display time on FinishBox's FinishTime text
        FinishCanvas.GetComponentInChildren<Text>().text = timer.ToString("0.00");
    }

    public void OnCounting() //Ctrl Button will invoke this function
    {
        startFlag = true;
    }

    public void OnReset()
    {
        startFlag = false;
        timer = 0;
    }
}
