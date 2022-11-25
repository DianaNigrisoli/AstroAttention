using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody controller;
    public float increment;
    public float speedForward;
    public float speedLateral;
    public Vector3 targetPos;
    public int playerPos;
    public static int score;
    public Boolean iscolliding ;
    public static int PortalCounter=0;
    public static List<float> ListReactionTime = new List<float>(10);

    void Awake()
    {
        score = 0;
        increment = 3;
        speedForward = 10;
        speedLateral = 5;
        targetPos = transform.position;
    }

    private void Start()
    {
        controller = GetComponent<Rigidbody>();
        iscolliding = false;
    }

    // Update is called once per frame
    void Update()
    {

        controller.velocity = transform.forward * speedForward;
        targetPos.z = controller.position.z;
        var step = speedLateral * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }
    
    private void OnTriggerEnter(Collider other)
    {   
        // Ho dovuto usare la variabile iscolliding perchè se no durava troppo l'evento 
        if (iscolliding)
        {
            PortalCounter += 1; 
            ListReactionTime.Add(FunctionTimer.reactionTime);
            print("score: " + score);
            print("Portal-spawn number: " + PortalCounter); 
            return;
        }
        // Check player position 
        if (targetPos.x == 0)
        {
            //check if the color is right
            if (ChangePortalColour_phase0.rightPortal2 == 1)
            {
                score += 1;
            }
        }
        if (targetPos.x > 0)
        {
            //check if the color is right
            if (ChangePortalColour_phase0.rightPortal3 == 1)
            {
                score += 1;
            }
        }
        if (targetPos.x < 0)
        {
            if (ChangePortalColour_phase0.rightPortal1 == 1)
            {
                score += 1;
            }
        }
        iscolliding = true;

    }

    private void OnTriggerExit(Collider other)
    {
        iscolliding = false;
    }

   
    public void MoveRight()
    {   FunctionTimer.leftLine = false;
        if (targetPos.x < increment)
        {
            targetPos += new Vector3(increment, 0, 0);
            
        }
        else
        {
            FunctionTimer.rightLine = true;
        }
        if (targetPos.x == 0)
        {
            FunctionTimer.leftLine = false;
            FunctionTimer.rightLine = false;
        }
    }
    public void MoveLeft()
    {
        FunctionTimer.rightLine = false;
        if (targetPos.x > -increment)
        {
            targetPos -= new Vector3(increment, 0, 0);
        }
        else
        {
            FunctionTimer.leftLine = true;
        }
        if (targetPos.x == 0)
        {
            FunctionTimer.leftLine = false;
            FunctionTimer.rightLine = false;
        }
    }
    
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Pumpkin"))
    //     {
    //         score++;
    //         AudioManager.instance.PlayCorrect();
    //         Destroy(other.gameObject);
    //     }
    //     else if (other.collider.CompareTag("FinishLine"))
    //     {
    //         Win();
    //     }
    // }

    // private void Win()
    // {
    //     GameManager.instance.score = this.score;
    //     SceneManager.LoadScene("FinalScene");
    // }
}
