using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
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
    public static int score;
    public static int PortalCounter=0;
    public static List<float> ListReactionTime = new List<float>(10);
    private static bool stop = false; 

    void Awake()
    {
        score = 0;
        increment = 3;
        speedForward = 10;
        speedLateral = 5;
        targetPos = transform.position;
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }
    
    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
    }
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.WaitForNext)
        {
            stop = true;
        }

        else
        {
            stop = false;
        }
    }

    private void Start()
    {
        controller = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
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

        else
        {
            controller.velocity = transform.forward * 0;
        }
    }

    private void OnTriggerEnter(Collider door)
    {
        if (door.gameObject.name != "Portals(Clone)")
            return;
       if (targetPos.x == 0)
        {
            //check if the color is right
            if (ChangePortalColour_phase0.rightPortal2 == 1 || ChangePortalColour_phase1.rightPortal2 == 1 ||
                ChangePortalColour_phase2.rightPortal2 == 1 || ChangePortalColour_phase3.rightPortal2 == 1)
            {
                score += 1;
            }
        }

        if (targetPos.x > 0)
        {
            //check if the color is right
            if (ChangePortalColour_phase0.rightPortal3 == 1 || ChangePortalColour_phase1.rightPortal3 == 1 ||
                ChangePortalColour_phase2.rightPortal3 == 1 || ChangePortalColour_phase3.rightPortal3 == 1)
            {
                score += 1;
            }
        }

        if (targetPos.x < 0)
        {
            if (ChangePortalColour_phase0.rightPortal1 == 1 || ChangePortalColour_phase1.rightPortal1 == 1 ||
                ChangePortalColour_phase2.rightPortal1 == 1 || ChangePortalColour_phase3.rightPortal1 == 1)
            {
                score += 1;
            }
        }

        ListReactionTime.Add(FunctionTimer.reactionTime);
        PortalCounter += 1;
        print("Portal-spawn number: " + PortalCounter);
        print("score: " + score);
        return;
    }
    // Ho dovuto usare la variabile iscolliding perchè se no durava troppo l'evento 
        
        
    
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
