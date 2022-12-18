using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts_FruitGame;
using TMPro;
using System;
using Assets.ScriptsDirectionGame;
using UnityEngine.UI;
using SkipTutorialButton = Assets.Scripts_FruitGame.SkipTutorialButton;

public class instructionsManager : MonoBehaviour
{
    List<Boolean> waitForUserInput = new List<bool>();
    private Boolean phaseStarted;
    public static bool showingTutorial;
    private static TutorialPreGamePhase tutorialPhase;
    
    private float waitTimer;
        
    private Boolean writing;
    private float writerTimer;
    private String currentPhaseTutorialText;
    private String displayedTutorialText;
    private String currentPhaseScreenText;
    //private String displayedIntroText;
    private Boolean showTargetingObjects;
    
    private List<int> IDs = new List<int>();
    //List<string> tutorialScreenTexts = new List<string>();
    private List<float> waitSeconds = new List<float>();
    private List<string> tutorialTargetingObject = new List<string>();
    private List<MyVector3> targetingObjectPositions = new List<MyVector3>();
    private List<MyVector3> targetingObjectRotations = new List<MyVector3>();
    
    public static event Action<TutorialPreGamePhase> OnTutorialPreGamePhaseChanged;
    
    //Instuctions
    [SerializeField] private GameObject playerSpaceship;
    [SerializeField] private TextMeshProUGUI instructionsText;
    [SerializeField] private RawImage txtBox;
    [SerializeField] private GameObject instructionsCanvas;
    List<string> istructionsList = new List<string>();

    private MiniGameStateFruit previousGameState;
    


    void Awake()
    {
        txtBox.enabled = true;
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }
    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.Instructions)
        {
            //Un-comment these two lines and comment the others if you don't want to show the tutorial
            // txtBox.enabled = false;
            // MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            Debug.Log("Starting phase0Tutorial");
            PrepareTutorialData();
            tutorialPhase = TutorialPreGamePhase.Zero;
            showingTutorial = true;
            playerSpaceship.SetActive(true);
        }
        else if (state == MiniGameStateFruit.Intro)
        {
            tutorialPhase = TutorialPreGamePhase.Zero;
            showingTutorial = false;
        }
        else previousGameState = state;
    }

    void Update()
    {
        if (showingTutorial)
        {
            switch (tutorialPhase)
            {
                case TutorialPreGamePhase.Zero:
                    HandlePhaseZero();
                    break;
                case TutorialPreGamePhase.One:
                    HandlePhaseOne();
                    break;
                case TutorialPreGamePhase.Two:
                    HandlePhaseTwo();
                    break;
                
                // case IntroPhase.Four:
                //     //HandlePhaseFour();
                //     break;
                

            }
            
        }
        
        
    }
    
    void HandlePhaseZero ()
    {
        if (!phaseStarted)
        {
            PrepareTextAndVariables();
        }
        else if(writing)
        {
            ShowTutorialScreenText();
        }
        else
        {
            WaitForInputOrTimer(nextPhase: TutorialPreGamePhase.One);
        }
            
    }
    
    
    void HandlePhaseOne ()
    {
        if (!phaseStarted)
        {
            PrepareTextAndVariables();
        }
        else if(writing)
        {
            ShowTutorialScreenText();
        }
        else
        {
            WaitForInputOrTimer(nextPhase: TutorialPreGamePhase.Two);
        }
            
    }
    
    void HandlePhaseTwo()
    {
        txtBox.enabled = false;
        showingTutorial = false;
        playerSpaceship.SetActive(true);
        MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
    }
    
    private void PrepareTutorialData()
        {
            string headerNames;
            var header = true;
            

            var path = GameManager.instance.Language == "ENG" ? "fruitMinigameTutorial" : "fruitMinigameTutorial_ita";

            TextAsset tutorialTextCsv = (TextAsset)Resources.Load(path);
            string tutorialLines = tutorialTextCsv.text;
            var lines = tutorialLines.Split('\n');
            foreach (string line in lines)
            {
                if (!header)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        var values = line.Split(';');
                        IDs.Add(int.Parse(values[0]));
                        istructionsList.Add(values[1]);
                        if (values[3] == "TRUE") waitForUserInput.Add(true);
                        else waitForUserInput.Add(false);
                        if (values[3] == "TRUE") waitForUserInput.Add(true);
                        else waitForUserInput.Add(false);
                        waitSeconds.Add(float.Parse(values[4]));
                        tutorialTargetingObject.Add(values[5]);
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
                }
                else
                {
                    headerNames = line;
                    header = false;
                }
            }
        }
    
    private void PrepareTextAndVariables()
    {
        Debug.Log("Starting tutorial phase " + tutorialPhase);
        int index = IDs.FindIndex(a => a.Equals((int)tutorialPhase));

        currentPhaseTutorialText = istructionsList[index];
        waitTimer = waitSeconds[index];

        instructionsText.SetText("");
        
        txtBox.enabled = true;
            
        //howTargetingObjects = false;
           
        displayedTutorialText = "";
        writerTimer = 0.0f;
        writing = true;
        phaseStarted = true;
            
            
    }
    
    private void ShowTutorialScreenText()
    {
        writerTimer += Time.deltaTime;
        if (writerTimer > 0.02 * displayedTutorialText.Length)
        {
            if (displayedTutorialText.Length < currentPhaseTutorialText.Length){
                displayedTutorialText += currentPhaseTutorialText[displayedTutorialText.Length];
            }
                
            else
            {
                writing = false;
            }
                
            instructionsText.SetText(displayedTutorialText);
            //screenText.SetText(displayedScreenText);
        }
    }
    
    private void WaitForInputOrTimer(TutorialPreGamePhase nextPhase)
    {
        waitTimer -= Time.deltaTime;
        bool condition;
        //if(introPhase == TutorialPhase.Five) condition = waitTimer < 0.0f;
        condition = Input.GetMouseButtonDown(0) || waitTimer < 0.0f;
        if (!condition) return;
        tutorialPhase = nextPhase;
        OnTutorialPreGamePhaseChanged?.Invoke(tutorialPhase);
        phaseStarted = false;
        txtBox.enabled = false;
        instructionsText.SetText("");
    }

    private void GoToWaitForNext()
    {
        waitTimer -= Time.deltaTime;
                        
        bool condition;
        condition = Input.GetMouseButtonDown(0) || waitTimer < 0.0f;
        if (!condition) return;
                        
        phaseStarted = false;
        txtBox.enabled = false;
        instructionsText.SetText("");
        playerSpaceship.SetActive(true);
        MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);   
    }
}
