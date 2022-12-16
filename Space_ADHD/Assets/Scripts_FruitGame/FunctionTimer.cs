using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer : MonoBehaviour
{
    public static float time;
    public static int counter;
    public static Boolean leftLine;
    public static Boolean rightLine;
    public static float reactionTime;

    public void Start()
    {
        time = 0;
        reactionTime = 0;
        counter = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.LeftArrow))||(PlayerMovement.swipedLeft)||(PlayerMovement.swipedRight))
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || (PlayerMovement.swipedLeft)) &&  (leftLine == false))
            {
                counter += 1;
                reactionTime = time;
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow) || (PlayerMovement.swipedRight)) &&  (rightLine == false))
            {
                counter += 1;
                reactionTime = time;
            }

        }
    }
}
