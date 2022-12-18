using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody controller;
    public float increment;
    public float speedForward;
    public float speedLateral;
    public Vector3 targetPos;
    public static int score;
    public static float time1portal;

    // For the entire game
    public static double[] reactionTimeMean = new double[4];
    public static double[] reactionTimeStd = new double[4];
    public static int[] errorsFruitsG = new int[4];
    public static double kidScoreFruitG = 0.0f; 
    public static bool isTut = false;
    
    // For the single phase
    public static int PortalCounter=0;
    public static List<float> ListReactionTime = new List<float>(Assets.Scripts_FruitGame.phase0Manager.NumSpawn);
    public static List<int> ListScore = new List<int>(Assets.Scripts_FruitGame.phase0Manager.NumSpawn);
    private static bool stop = false; 
    
    [SerializeField] Sprite[] indicators;
    [SerializeField] private Image answerIndicator;
    [SerializeField] public GameObject IndicatorCanvas;

    public AudioSource wrongSound;
    public AudioSource rightSound;

    [SerializeField] private AudioClip wrong_clip;
    [SerializeField] private AudioClip right_clip;
    
    //Swipe movement
    // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
    public const float MAX_SWIPE_TIME = 0.5f; 
	
    // Factor of the screen width that we consider a swipe
    // 0.17 works well for portrait mode 16:9 phone
    public const float MIN_SWIPE_DISTANCE = 0.17f;

    public static bool swipedRight = false;
    public static bool swipedLeft = false;
    
    public bool debugWithArrowKeys = true;

    private float startTime;
    private Vector2 startFingerPosition;
    
    void Awake()
    {
        score = 0;
        kidScoreFruitG = 0.0f;
        increment = 3.4f; //prima 3
        speedForward = 20;
        speedLateral = 7; // prima 5
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
        if ( state == MiniGameStateFruit.Intro  || state == MiniGameStateFruit.WaitForNext || state == MiniGameStateFruit.Instructions ||
             state == MiniGameStateFruit.End)
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
        
        wrongSound = gameObject.AddComponent<AudioSource>();
        wrongSound.clip = wrong_clip;
        wrongSound.volume = 0.05f;
        wrongSound.playOnAwake = false;
        
        rightSound = gameObject.AddComponent<AudioSource>();
        rightSound.clip = right_clip;
        rightSound.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        swipedRight = false;
        swipedLeft = false;
        if (!stop)
        {
            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    startFingerPosition = new Vector2(t.position.x / (float)Screen.width,
                        t.position.y / (float)Screen.width);
                    startTime = Time.time;
                }

                if(t.phase == TouchPhase.Ended)
                {
                    if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
                        return;

                    Vector2 endFingerPosition = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);

                    Vector2 swipe = new Vector2(endFingerPosition.x - startFingerPosition.x, endFingerPosition.y - startFingerPosition.y);

                    if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                        return;

                    if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) { // Horizontal swipe
                        if (swipe.x > 0) {
                            swipedRight = true;
                        }
                        else {
                            swipedLeft = true;
                        }
                    }
                }
            }
            
            controller.velocity = transform.forward * speedForward;
            targetPos.z = controller.position.z;
            var step = speedLateral * Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.RightArrow)) || (swipedRight))
            {
                MoveRight();
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (swipedLeft))
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
        time1portal = FunctionTimer.time;
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
                rightSound.Play();
                if (!isTut)
                {
                    ListScore.Add(1);
                    ListReactionTime.Add(FunctionTimer.reactionTime);
                }

            }
            else
            {
                answerIndicator.sprite = indicators[1];
                wrongSound.Play();
                if (!isTut)
                {
                    ListScore.Add(0);
                    ListReactionTime.Add(FunctionTimer.reactionTime);
                }
            }
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
                rightSound.Play();
                if (!isTut)
                {
                    ListScore.Add(1);
                    ListReactionTime.Add(FunctionTimer.reactionTime);
                }
            }
            else
            {
                answerIndicator.sprite = indicators[1];
                wrongSound.Play();
                if (!isTut)
                {
                    ListScore.Add(0);
                    ListReactionTime.Add(FunctionTimer.reactionTime);
                }
            }
                
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
                rightSound.Play();
                if (!isTut)
                {
                    ListScore.Add(1);
                    ListReactionTime.Add(FunctionTimer.reactionTime);
                }
                
            }
            else 
            {
                
                answerIndicator.sprite = indicators[1];
                wrongSound.Play();
                if (!isTut)
                {
                    ListScore.Add(0);
                    ListReactionTime.Add(FunctionTimer.reactionTime);
                }
            }
        }

        
        PortalCounter += 1;
        string t = "";
        string s = "";
        foreach (var t1 in ListReactionTime)
        {
            t += t1.ToString() + ", ";
        }
        foreach (var s1 in ListScore)
        {
            s += s1.ToString() + ", ";
        }
        print("list reaction times: " + t);
        print("list scores: " + s);
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
