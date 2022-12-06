using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts_FruitGame;
using Assets.ScriptsDirectionGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts_FruitGame
{

    public class IntroManager : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialCanvas;
        [SerializeField] private TextMeshProUGUI introText;
        [SerializeField] private RawImage txtBox;
        //[SerializeField] private RawImage fruitTable;
        [SerializeField] private GameObject tutorialRobotPrefab;
        private GameObject tutorialRobot;
        
        List<Boolean> waitForUserInput = new List<bool>();
        private Boolean phaseStarted;
        
        private bool showingTutorial;
        private IntroPhase introPhase;

        /*Variables to manage tutorial phases*/
        List<int> IDs = new List<int>();
        List<string> tutorialRobotTexts = new List<string>();
        //List<string> tutorialScreenTexts = new List<string>();
        List<float> waitSeconds = new List<float>();
        List<string> tutorialTargetingObject = new List<string>();
        List<MyVector3> targetingObjectPositions = new List<MyVector3>();
        List<MyVector3> targetingObjectRotations = new List<MyVector3>();
        
        
        private float waitTimer;
        
        private Boolean writing;
        private float writerTimer;
        private String currentPhaseIntroRobotText;
        private String displayedIntroRobotText;
        private String currentPhaseScreenText;
        private String displayedIntroText;
        private Boolean showTargetingObjects;
        
        
        public static event Action<IntroPhase> OnIntroPhaseChanged;
        
        void Awake()
        {
            MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        }

        void OnDestroy()
        {
            MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
        }

        private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
        {
            if (state == MiniGameStateFruit.Intro)
            {
                //Un-comment these two lines and comment the others if you don't want to show the tutorial
                txtBox.enabled = false;
                MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
                // Debug.Log("Starting Tutorial");
                // PrepareTutorialData();
                // introPhase = IntroPhase.Zero;
                // showingTutorial = true;
                
            }
        }

       
        void Update()
        {
            if (showingTutorial)
            {
                switch (introPhase)
                {
                    case IntroPhase.Zero:
                        HandlePhaseZero();
                        break;
                    case IntroPhase.One:
                        //HandlePhaseOne();
                        break;
                    // case IntroPhase.Two:
                    //     //HandlePhaseTwo();
                    //     break;
                    // case IntroPhase.Three:
                    //     //HandlePhaseThree();
                    //     break;
                    // case IntroPhase.Four:
                    //     //HandlePhaseFour();
                    //     break;
                    
                    //Da continuare fino a phase 6 (per ora) 
                    
                }
                
                
            }

            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            Destroy(this);
        }

        
        void HandlePhaseZero ()
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
                WaitForInputOrTimer(nextPhase: IntroPhase.One);
            }
            
        }

        void HandlePhaseOne ()
        {
            
        }

        void HandlePhaseTwo()
        {
            
        }

        void HandlePhaseThree()
        {
            
        }

        void HandlePhaseFour()
        {
            
        }
        
        //****************************************************************************************
        private void PrepareTutorialData()
        {
            string headerNames;
            Boolean header = true;

            string path = Application.dataPath + "/Resources/fruitMinigameIntro.csv";
            
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
        
        
        //******************************************************************************************
        private void PrepareRobotAndTextAndVariables()
        {
            Debug.Log("Starting tutorial phase " + introPhase);
            int index = IDs.FindIndex(a => a.Equals((int)introPhase));

            currentPhaseIntroRobotText = tutorialRobotTexts[index];
            //currentPhaseScreenText = tutorialScreenTexts[index];
            waitTimer = waitSeconds[index];

            introText.SetText("");
            //screenText.SetText("");
            //screenTargeting.enabled = false;
            tutorialRobot = Instantiate(tutorialRobotPrefab, GameObject.Find("All").transform);
            tutorialRobot.transform.Rotate(-3.611f, -154.285f, 0);
            tutorialRobot.transform.position = new Vector3(0.115f, 1.362f, 0.937f);

            AudioSource[] tutorialRobotVoices;
            tutorialRobotVoices = tutorialRobot.GetComponents<AudioSource>();
            Random r = new Random();
            int rInt = r.Next(0, tutorialRobotVoices.Length);
            tutorialRobotVoices[rInt].Play();
            txtBox.enabled = true;
            
            //howTargetingObjects = false;
           
            displayedIntroRobotText = "";
            writerTimer = 0.0f;
            writing = true;
            phaseStarted = true;
            
            
        }
        
        //********************************************************************************************
        private void ShowTutorialRobotAndScreenText()
        {
            writerTimer += Time.deltaTime;
            if (writerTimer > 0.02 * displayedIntroRobotText.Length)
            {
                if (displayedIntroRobotText.Length < currentPhaseIntroRobotText.Length){
                    displayedIntroRobotText += currentPhaseIntroRobotText[displayedIntroRobotText.Length];
                }else if (displayedIntroText.Length < currentPhaseScreenText.Length)
                {
                    displayedIntroText += currentPhaseScreenText[displayedIntroText.Length];
                }
                else
                {
                    writing = false;
                }
                
                introText.SetText(displayedIntroRobotText);
                //screenText.SetText(displayedScreenText);
            }
        }
        
        private void WaitForInputOrTimer(IntroPhase nextPhase)
        {
            waitTimer -= Time.deltaTime;
            bool condition;
            //if(introPhase == TutorialPhase.Five) condition = waitTimer < 0.0f;
            condition = Input.GetMouseButtonDown(0) || waitTimer < 0.0f;
            if (!condition) return;
            introPhase = nextPhase;
            OnIntroPhaseChanged?.Invoke(introPhase);
            Destroy(tutorialRobot);
            phaseStarted = false;
            txtBox.enabled = false;
            introText.SetText("");
        }
    }
}
