using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    
    [SerializeField] Sprite[] indicators;
    [SerializeField] private Image answerIndicator;
    [SerializeField] public GameObject IndicatorCanvas;

    void Awake()
    {
        score = 0;
        increment = 3;
        speedForward = 10;
        speedLateral = 5;
        targetPos = transform.position;
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        answerIndicator = GameObject.Find("AnswerIndicator").GetComponent<Image>();
    }
    
    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
    }
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if ( state == MiniGameStateFruit.Intro  || state == MiniGameStateFruit.WaitForNext || state == MiniGameStateFruit.Instructions)
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
        IndicatorCanvas.SetActive(true);
        if (targetPos.x == 0)
        {
            //check if the color is right
            if (ChangePortalColour_phase0.rightPortal2 == 1 || ChangePortalColour_phase1.rightPortal2 == 1 ||
                ChangePortalColour_phase2.rightPortal2 == 1 || ChangePortalColour_phase3.rightPortal2 == 1 ||
                ChangePortalColour_phase0Tutorial.rightPortal2 == 1|| ChangePortalColour_phase1Tutorial.rightPortal2 == 1 || 
                ChangePortalColour_phase2Tutorial.rightPortal2 == 1 || ChangePortalColour_phase3Tutorial.rightPortal2 == 1)

            {
                score += 1;
                answerIndicator.sprite = indicators[0];

            }
            else answerIndicator.sprite = indicators[1];
        }
    

        if (targetPos.x > 0)
        {
            //check if the color is right
            if (ChangePortalColour_phase0.rightPortal3 == 1 || ChangePortalColour_phase1.rightPortal3 == 1 ||
                ChangePortalColour_phase2.rightPortal3 == 1 || ChangePortalColour_phase3.rightPortal3 == 1 ||
                ChangePortalColour_phase0Tutorial.rightPortal3 == 1 || ChangePortalColour_phase1Tutorial.rightPortal3 == 1 ||
                ChangePortalColour_phase2Tutorial.rightPortal3 == 1 || ChangePortalColour_phase3Tutorial.rightPortal3 == 1)
            {
                score += 1;
                answerIndicator.sprite = indicators[0];
            }
            else answerIndicator.sprite = indicators[1];
        }

        if (targetPos.x < 0)
        {
            if (ChangePortalColour_phase0.rightPortal1 == 1 || ChangePortalColour_phase1.rightPortal1 == 1 ||
                ChangePortalColour_phase2.rightPortal1 == 1 || ChangePortalColour_phase3.rightPortal1 == 1 ||
                ChangePortalColour_phase0Tutorial.rightPortal1 == 1 || ChangePortalColour_phase1Tutorial.rightPortal1 == 1 ||
                ChangePortalColour_phase2Tutorial.rightPortal1 == 1 ||ChangePortalColour_phase3Tutorial.rightPortal1 == 1)
            {
                score += 1;
                answerIndicator.sprite = indicators[0];
            }
            else answerIndicator.sprite = indicators[1];
        }

        ListReactionTime.Add(FunctionTimer.reactionTime);
        PortalCounter += 1;
        print("Portal-spawn number: " + PortalCounter);
        print("score: " + score);
        return;
    }
    // Ho dovuto usare la variabile iscolliding perchè se no durava troppo l'evento 

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        IndicatorCanvas.SetActive(false);
    }
    private void OnTriggerExit(Collider door)
    {   
        if (door.gameObject.name != "Portals(Clone)")
            return;
        StartCoroutine(waiter());
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
