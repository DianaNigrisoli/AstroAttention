using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer : MonoBehaviour
{
    //NON STAMPA!!!!!!
    
    // public float timeRemaining;
    // public bool timerIsRunning = false;

    // public void Start()
    // {
    //     if (timerIsRunning)
    //     {
    //         if (timeRemaining > 0)
    //         {
    //             timeRemaining -= Time.deltaTime;
    //             Debug.Log("Time remaining: " + timeRemaining);
    //         }
    //         else
    //         {
    //             Debug.Log("Time's up!");
    //         }
    //     }
    // }
    public float time=0;
    public int Counter = 0;

    public float reactionTime;
    private int positionPortal;
    private int positionPlayer_start;
    public void Start()
    {
        positionPlayer_start= (int)GameObject.Find("Player").transform.position.z;
        positionPortal = positionPlayer_start+ 50;
    }

    private void Update()
    {
        float positionPlayer = GameObject.Find("Player").transform.position.z;

        time += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            Counter += 1;
            reactionTime = time;
        }
        
        if (positionPlayer == positionPortal)
        {
            print("Counter: " + Counter);
            print("React Time: "+ reactionTime);
        }
    }
    
    
}
