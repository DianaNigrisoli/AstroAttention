using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts_A_General;
using Assets.ScriptsDirectionGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = System.Random;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private RawImage txtBox;

    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private RawImage txtBoxScreen;
    [SerializeField] private RawImage screenTargeting;
    private Graphic screenTargetingGraphics;
    
    [SerializeField] private GameObject tutorialRobotPrefab;
    private GameObject tutorialRobot;
    
    [SerializeField] private GameObject tutorialRingButtonsPrefab;
    private List<GameObject> tutorialRingButtons;
    private List<Vector3> tutorialRingButtonsPositions;

    [SerializeField] private GameObject tutorialRingSectorsPrefab;
    private List<GameObject> tutorialRingSectors;
    private List<Vector3> tutorialRingSectorsScales;

    private SortedList<int, TutorialPhaseData> tutorialPhaseData;
    private Boolean showingTutorial;
    private float rotationY = 30.0f;
    private Vector3 tutorialRingStartPosition;
    
    private GameObject shootingStarNoTail;
    private bool spawnedStar;
    public static bool touch;
    
    /*Variables to manage tutorial phases*/
    List<int> IDs = new List<int>();
    List<string> tutorialRobotTexts = new List<string>();
    List<string> tutorialScreenTexts = new List<string>();
    List<Boolean> waitForUserInput = new List<bool>();
    List<float> waitSeconds = new List<float>();
    List<string> tutorialTargetingObject = new List<string>();
    List<MyVector3> targetingObjectPositions = new List<MyVector3>();
    List<MyVector3> targetingObjectRotations = new List<MyVector3>();

    private Boolean phaseStarted;
    private float waitTimer;

    private Boolean writing;
    private float writerTimer;
    private String currentPhaseTutorialRobotText;
    private String displayedTutorialRobotText;
    private String currentPhaseScreenText;
    private String displayedScreenText;
    private Boolean showTargetingObjects;
    /**/

    private TutorialPhase tutorialPhase;
    
    void Awake()
    {
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }
    
    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.Intro)
        {
            Debug.Log("Starting Tutorial");
            PrepareTutorialData();
            
            // TODO: it seems that the built in canvas scaler works fine, eventually change this code
            // tutorialText.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.17f, 0);
            // txtBox.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.30f, -1);
            // txtBoxScreen.transform.position = new Vector3(Screen.width / 2f, Screen.height / 15f, 0);
            // screenText.transform.position = new Vector3(Screen.width / 2f, Screen.height / 15f, 0);

            screenTargetingGraphics = screenTargeting.GetComponent<Graphic>();
            shootingStarNoTail = GameObject.Find("notShootingStar");
            tutorialPhase = TutorialPhase.Zero;
            showingTutorial = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showingTutorial)
        {
            switch (tutorialPhase)
            {
                case TutorialPhase.Zero:
                    HandlePhaseZero();
                    break;
                case TutorialPhase.One:
                    HandlePhaseOne();
                    break;
                case TutorialPhase.Two:
                    for(int i=0; i<tutorialRingSectors.Count; i++)
                        Destroy(tutorialRingSectors[i]);
                    
                    HandlePhaseTwo();
                    break;
                case TutorialPhase.Three:
                    for (int i = 0; i < tutorialRingButtons.Count; i++)
                        Destroy(tutorialRingButtons[i]);

                    HandlePhaseThree();
                    break;
                case TutorialPhase.Four:
                    HandlePhaseFour();
                    break;
                case TutorialPhase.Five:
                    HandlePhaseFive();
                    break;
                case TutorialPhase.Six:
                    for (int i = 0; i < tutorialRingButtons.Count; i++)
                        Destroy(tutorialRingButtons[i]);
                    MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
                    Destroy(this);
                    break;
                case TutorialPhase.Seven:
                    break;
                case TutorialPhase.Eight:
                    break;
                case TutorialPhase.Nine:
                    break;
                case TutorialPhase.Ten:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void HandlePhaseFive()
    {
        if (!phaseStarted)
        {
            PrepareRobotAndTextAndVariables();
        }
        else if(writing)
        {
            ShowTutorialRobotAndScreenText();
        }
        else if(!spawnedStar)
        {
            spawnShootingStar();
        }else if (!showTargetingObjects)
        {
            txtBox.enabled = false;
            tutorialText.SetText("");
            showTargetingObjects = true;

            GameObject ring = Instantiate(tutorialRingButtonsPrefab, new Vector3(-0.094f, 1.379f, 1.127f),
                Quaternion.identity, GameObject.Find("All").transform);
            tutorialRingButtons.Add(ring);
            tutorialRingButtonsPositions.Add(ring.transform.position);
            int i = 0;
            for (; i < tutorialRingButtons.Count; i++)
            {
                tutorialRingButtons[i].transform.Rotate(-7.772f, 0, 0);
                tutorialRingButtons[i].transform.localScale = new Vector3(0.17558f, 0.17558f, 0.17558f);
            }
        }else if(!touch)
        {
            for (int i = 0; i < tutorialRingButtons.Count; i++)
            {
                tutorialRingButtons[i].transform.Rotate(0, rotationY*Time.deltaTime, 0);
                tutorialRingButtons[i].transform.position = tutorialRingButtonsPositions[i] + 
                                                            new Vector3(0.0f, Mathf.Sin(Time.time*3f)/250f, 0.0f);
            }
            
            // 
        }
        else
        {
            shootingStarNoTail.transform.position = new Vector3(-12.75f, 5.3f, 16.4f);;
            WaitForInputOrTimer(TutorialPhase.Six);
        }
    }

    private void spawnShootingStar()
    {
        if (tutorialPhase == TutorialPhase.Five)
        {
            Vector3 spawnPosition = new Vector3(-2.75f, 5.3f, 16.4f);
            shootingStarNoTail.transform.position = spawnPosition;
            spawnedStar = true;
        }
        
    }

    private void HandlePhaseFour()
    {
        if (!phaseStarted)
        {
            PrepareRobotAndTextAndVariables();
        }
        else if(writing)
        {
            ShowTutorialRobotAndScreenText();
        }
        else
        {
            screenTargeting.enabled = true;
            screenTargetingGraphics.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
            WaitForInputOrTimer(nextPhase: TutorialPhase.Five);
        }
    }

    private void HandlePhaseThree()
    {
        if (!phaseStarted)
        {
            PrepareRobotAndTextAndVariables();
        }
        else if(writing)
        {
            ShowTutorialRobotAndScreenText();
        }
        else
        {
            WaitForInputOrTimer(nextPhase: TutorialPhase.Four);
        }
    }

    private void HandlePhaseTwo()
    {
        if (!phaseStarted)
        {
            PrepareRobotAndTextAndVariables();
        }else if (writing)
        {
            ShowTutorialRobotAndScreenText();
        }
        else if (!showTargetingObjects)
        {
            waitTimer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) || waitTimer < 0.0f)
            {
                txtBox.enabled = false;
                tutorialText.SetText("");
                showTargetingObjects = true;

                GameObject ring = Instantiate(tutorialRingButtonsPrefab, new Vector3(-0.094f, 1.379f, 1.127f),
                     Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingButtons.Add(ring);
                tutorialRingButtonsPositions.Add(ring.transform.position);
                ring = Instantiate(tutorialRingButtonsPrefab, new Vector3(+0.094f, 1.379f, 1.127f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingButtons.Add(ring);
                tutorialRingButtonsPositions.Add(ring.transform.position);

                int i = 0;
                for (; i < tutorialRingButtons.Count; i++)
                {
                    tutorialRingButtons[i].transform.Rotate(-7.772f, 0, 0);
                    tutorialRingButtons[i].transform.localScale = new Vector3(0.17558f, 0.17558f, 0.17558f);
                }
                
                ring = Instantiate(tutorialRingButtonsPrefab, new Vector3(-0.089f, 1.384f, 0.912f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingButtons.Add(ring);
                tutorialRingButtonsPositions.Add(ring.transform.position);
                ring = Instantiate(tutorialRingButtonsPrefab, new Vector3(+0.089f, 1.384f, 0.912f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingButtons.Add(ring);
                tutorialRingButtonsPositions.Add(ring.transform.position);

                for (; i < tutorialRingButtons.Count; i++)
                {
                    tutorialRingButtons[i].transform.Rotate(-7.356f, 0, 0);
                    tutorialRingButtons[i].transform.localScale = new Vector3(0.146223f, 0.146223f, 0.146223f);
                }
                
            }
        }
        else
        {
            float scaleFactor = 20f;
            tutorialRobot.transform.position = Vector3.MoveTowards(tutorialRobot.transform.position, new Vector3(0.19f, 1.358f, 0.868f),
                Time.deltaTime * 0.2f);
            for (int i = 0; i < tutorialRingButtons.Count; i++)
            {
                tutorialRingButtons[i].transform.Rotate(0, rotationY*Time.deltaTime, 0);
                tutorialRingButtons[i].transform.position = tutorialRingButtonsPositions[i] + 
                                                            new Vector3(0.0f, Mathf.Sin(Time.time*3f)/250f, 0.0f);
            }
            WaitForInputOrTimer(TutorialPhase.Three);
            
        }
    }

    private void HandlePhaseOne()
    {
        if (!phaseStarted)
        {
            PrepareRobotAndTextAndVariables();
        }else if (writing)
        {
            ShowTutorialRobotAndScreenText();
        }
        else if (!showTargetingObjects)
        {
            waitTimer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) || waitTimer < 0.0f)
            {
                txtBox.enabled = false;
                tutorialText.SetText("");
                showTargetingObjects = true;
                GameObject ring = Instantiate(tutorialRingSectorsPrefab, new Vector3(-1.04f, 3.133f, 6.709461f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingSectors.Add(ring);
                tutorialRingSectorsScales.Add(ring.transform.localScale);
                ring = Instantiate(tutorialRingSectorsPrefab, new Vector3(+1.04f, 3.133f, 6.709461f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingSectors.Add(ring);
                tutorialRingSectorsScales.Add(ring.transform.localScale);
                ring = Instantiate(tutorialRingSectorsPrefab, new Vector3(-1.04f, 1.367f, 6.709461f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingSectors.Add(ring);
                tutorialRingSectorsScales.Add(ring.transform.localScale);
                ring = Instantiate(tutorialRingSectorsPrefab, new Vector3(+1.04f, 1.367f, 6.709461f),
                    Quaternion.identity, GameObject.Find("All").transform);
                tutorialRingSectors.Add(ring);
                tutorialRingSectorsScales.Add(ring.transform.localScale);
            }
        }
        else
        {
            float scaleFactor = 20f;
            for (int i = 0; i < tutorialRingSectors.Count; i++)
            {
                tutorialRingSectors[i].transform.Rotate(0, 0, rotationY*Time.deltaTime);
                tutorialRingSectors[i].transform.localScale = tutorialRingSectorsScales[i] +
                                                              new Vector3(Mathf.Sin(Time.time*3f)/scaleFactor, 
                                                                  Mathf.Sin(Time.time*3f)/scaleFactor, 
                                                                  Mathf.Sin(Time.time*3f)/scaleFactor);
            }
            WaitForInputOrTimer(TutorialPhase.Two);
            
        }
    }

    private void HandlePhaseZero()
    {
        if (!phaseStarted)
        {
            PrepareRobotAndTextAndVariables();
        }
        else if(writing)
        {
            ShowTutorialRobotAndScreenText();
        }
        else
        {
           WaitForInputOrTimer(nextPhase: TutorialPhase.One);
        }
        
    }
    
    private void WaitForInputOrTimer(TutorialPhase nextPhase)
    {
        waitTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) || waitTimer < 0.0f)
        {
            tutorialPhase = nextPhase;
            Destroy(tutorialRobot);
            phaseStarted = false;
            txtBox.enabled = false;
            tutorialText.SetText("");
        }
    }
    
    private void PrepareRobotAndTextAndVariables()
    {
        int index = IDs.FindIndex(a => a.Equals((int) tutorialPhase));
            
        currentPhaseTutorialRobotText = tutorialRobotTexts[index];
        currentPhaseScreenText = tutorialScreenTexts[index];
        waitTimer = waitSeconds[index];
            
        tutorialText.SetText("");
        screenText.SetText("");
        screenTargeting.enabled = false;

        if (tutorialPhase != TutorialPhase.Five)
        {
            tutorialRobot = Instantiate(tutorialRobotPrefab, GameObject.Find("All").transform);
            tutorialRobot.transform.Rotate(-3.611f, -154.285f, 0);
            tutorialRobot.transform.position = new Vector3(0.115f, 1.362f, 0.937f);

            AudioSource[] tutorialRobotVoices;
            tutorialRobotVoices = tutorialRobot.GetComponents<AudioSource>();
            Random r = new Random();
            int rInt = r.Next(0, tutorialRobotVoices.Length);
            tutorialRobotVoices[rInt].Play();
            txtBox.enabled = true;
        }
        
        showTargetingObjects = false;
        tutorialRingSectors = new List<GameObject>();
        tutorialRingSectorsScales = new List<Vector3>();
        
        tutorialRingButtons = new List<GameObject>();
        tutorialRingButtonsPositions = new List<Vector3>();
        
        displayedTutorialRobotText = "";
        displayedScreenText = "";
        writerTimer = 0.0f;
        writing = true;
        phaseStarted = true;
        spawnedStar = false;
        touch = false;

        //TODO: adjust font sizes
        switch (tutorialPhase)
        {
            case TutorialPhase.Zero:
                tutorialText.fontSize = 28;
                break;
            case TutorialPhase.One:
                tutorialText.fontSize = 24;
                break;
        }
    }
    private void ShowTutorialRobotAndScreenText()
    {
        writerTimer += Time.deltaTime;
        if (writerTimer > 0.05 * displayedTutorialRobotText.Length)
        {
            if (displayedTutorialRobotText.Length < currentPhaseTutorialRobotText.Length){
                displayedTutorialRobotText += currentPhaseTutorialRobotText[displayedTutorialRobotText.Length];
            }else if (displayedScreenText.Length < currentPhaseScreenText.Length)
            {
                displayedScreenText += currentPhaseScreenText[displayedScreenText.Length];
            }
            else
            {
                writing = false;
            }
            tutorialText.SetText(displayedTutorialRobotText);
            screenText.SetText(displayedScreenText);
        }
    }
    
    
    private void PrepareTutorialData()
    {
        string headerNames;
        Boolean header = true;

        string path = Application.dataPath + "/Resources/directionGameTutorialData.csv";
        
        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                //Debug.Log(line);
                if (!header)
                {
                    IDs.Add(int.Parse(values[0]));
                    //Debug.Log(IDs[IDs.Count - 1]);
                    tutorialRobotTexts.Add(values[1]);
                    //Debug.Log(tutorialRobotTexts[tutorialRobotTexts.Count - 1]);
                    tutorialScreenTexts.Add(values[2]);
                    //Debug.Log(tutorialScreenTexts[tutorialScreenTexts.Count - 1]);
                    
                    if (values[3] == "TRUE") waitForUserInput.Add(true);
                    else waitForUserInput.Add(false);
                    //Debug.Log(waitForUserInput[tutorialScreenTexts.Count - 1]);
                    
                    waitSeconds.Add(float.Parse(values[4]));
                    //Debug.Log(waitSeconds[waitSeconds.Count - 1]);
                    
                    tutorialTargetingObject.Add(values[5]);
                    //Debug.Log(tutorialTargetingObject[tutorialTargetingObject.Count - 1]);

                    try
                    {
                        targetingObjectPositions.Add(new MyVector3(float.Parse(values[6]), float.Parse(values[7]), float.Parse(values[8])));
                    }
                    catch (FormatException)
                    {
                        targetingObjectPositions.Add(new MyVector3(-1f, -1f, -1f));
                    }
                    try
                    {
                        targetingObjectRotations.Add(new MyVector3(float.Parse(values[9]), float.Parse(values[10]), float.Parse(values[11])));
                    }
                    catch (FormatException)
                    {
                        targetingObjectRotations.Add(new MyVector3(0f, 0f, 0f));
                    }
                }
                else
                {
                    headerNames = line;
                    header = false;
                }
            }
        }
    }
}
