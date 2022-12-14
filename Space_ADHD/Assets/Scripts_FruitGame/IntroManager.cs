using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts_FruitGame;
//using Assets.ScriptsDirectionGame;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts_FruitGame
{

    public class IntroManager : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialCanvas;
        [SerializeField] private GameObject tableCanvas;
        [SerializeField] private GameObject fruitCanvas;
        [SerializeField] private TextMeshProUGUI introText;
        [SerializeField] private RawImage txtBox;
        [SerializeField] private Image fruitImage;
        [SerializeField] private Sprite banana;
        [SerializeField] private Image fruitTable;
        [SerializeField] private Sprite table_ita;
        [SerializeField] private Sprite table_ing;
        [SerializeField] private GameObject tutorialRobotPrefab;
        [SerializeField] private GameObject playerSpaceship;
        [SerializeField] private GameObject tutorialRing;
        [SerializeField] private GameObject PortalsIntro;
        
        private GameObject tutorialRobot;
        private GameObject ring;
        private GameObject portals;
        
        List<Boolean> waitForUserInput = new List<bool>();
        private Boolean phaseStarted;
        
        public static bool showingTutorial;
        private IntroPhase introPhase;

        /*Variables to manage tutorial phases*/
        List<int> IDs = new List<int>();
        List<string> tutorialRobotTexts = new List<string>();
        //List<string> tutorialScreenTexts = new List<string>();
        List<float> waitSeconds = new List<float>();
        
        //***NON serve! Cancellare 
        List<string> tutorialTargetingObject = new List<string>();
        // List<MyVector3> targetingObjectPositions = new List<MyVector3>();
        // List<MyVector3> targetingObjectRotations = new List<MyVector3>();
        private Boolean showTargetingObjects;
        //***
        
        private float waitTimer;
        private float waitTimer2; //timer for showing the fruit image/table.
        
        private const float RotationZ = 30.0f;
        
        private Boolean writing;
        private float writerTimer;
        private String currentPhaseIntroRobotText;
        private String displayedIntroRobotText;
        private String currentPhaseScreenText;
        private Boolean showImg = false;



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
                // txtBox.enabled = false;
                // MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
                Debug.Log("Starting Tutorial");
                PrepareTutorialData();
                introPhase = IntroPhase.Zero;
                showingTutorial = true;
                playerSpaceship.SetActive(false);
                tableCanvas.SetActive(false);
                fruitCanvas.SetActive(false);
                

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
                        HandlePhaseOne();
                        break;
                    case IntroPhase.Two:
                        HandlePhaseTwo();
                        break;
                    case IntroPhase.Three:
                        HandlePhaseThree();
                        break;
                    case IntroPhase.Four:
                        HandlePhaseFour();
                        break;
                    case IntroPhase.Five:
                        HandlePhaseFive();
                        break;
                    case IntroPhase.Six:
                        HandlePhaseSix();
                        break;
                    
                    
                }
                
                
            }
            
            else if (SkipTutorialButton.TutorialSkipped && !showingTutorial)
            {
                txtBox.enabled = false;
                introText.SetText("");
                Destroy(tutorialRobot);
                Destroy(portals); 
                //fruitCanvas.SetActive(false);
                playerSpaceship.SetActive(true);
            }
            
            //Destroy(this);
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
            if (!phaseStarted)
            {
                PrepareRobotAndTextAndVariables();
            }
            else if(writing)
            {
                ShowTutorialRobotAndScreenText();
                showImg = true;
            }
            else if (showImg)
            {
                ShowTableImage(fruitCanvas, fruitImage, banana);
                waitTimer2 = 10.0f;
            }
            else
            {
                MoveRing();
                DestroyandExit(nextPhase:IntroPhase.Two, canvas: fruitCanvas);
            }
           
        }

        void HandlePhaseTwo()
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
                WaitForInputOrTimer(nextPhase: IntroPhase.Three);
            }
        }

        void HandlePhaseThree()
        {

            if (!phaseStarted)
            {
                PrepareRobotAndTextAndVariables();
            }
            else if(writing)
            {
                ShowTutorialRobotAndScreenText();
                showImg = true;
            }
            else if (showImg)
            {
                if (GameManager.instance.Language == "ENG") ShowTableImage(tableCanvas, fruitTable, table_ing);
                else ShowTableImage(tableCanvas, fruitTable, table_ita);
                ;
                waitTimer2 = 20.0f;
            }
            else
            {
                DestroyandExit(nextPhase: IntroPhase.Four, canvas: tableCanvas);
            }
            
        }
        
        void HandlePhaseFour()
        {
            if (!phaseStarted)
            {
                PrepareRobotAndTextAndVariables();
            }
            else if(writing)
            {
                ShowTutorialRobotAndScreenText();
                showImg = true;
                waitTimer2 = 10.0f;
            }
            else if (showImg)
            {
                ShowPortals();
            }
            else
            {
                tutorialRobot.transform.position = Vector3.MoveTowards(tutorialRobot.transform.position, new Vector3(4f, -0.68f, 2.7f),
                 Time.deltaTime * 1.5f); 
                WaitForInputOrTimer(nextPhase: IntroPhase.Five);
            }
        }
        
        void HandlePhaseFive()
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
                WaitForInputOrTimer(nextPhase: IntroPhase.Six);
            }
        }
        
        void HandlePhaseSix()
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
                waitTimer -= Time.deltaTime;
                bool condition;
                //if(introPhase == TutorialPhase.Five) condition = waitTimer < 0.0f;
                condition = Input.GetMouseButtonDown(0) || waitTimer < 0.0f;
                if (!condition) return;
                Destroy(tutorialRobot);
                phaseStarted = false;
                txtBox.enabled = false;
                introText.SetText("");
                txtBox.enabled = false;
                showingTutorial = false;
                playerSpaceship.SetActive(true);
                tableCanvas.SetActive(false);
                MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            }
        }
        
        
        //****************************************************************************************
        private void PrepareTutorialData()
        {
           var header = true;
           
           var path = GameManager.instance.Language == "ENG" ? "fruitMinigameIntro" : "fruitMinigameIntro_ita";
           
           TextAsset tutorialTextCsv = (TextAsset) Resources.Load(path);
           string tutorialLines = tutorialTextCsv.text;
           var lines = tutorialLines.Split('\n');
           Debug.Log(tutorialLines);
           
           foreach (string line in lines)
            {
                Debug.Log(lines);
                if (!header)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        var values = line.Split(';');
                        IDs.Add(int.Parse(values[0]));
                        tutorialRobotTexts.Add(values[1]);
                        if (values[3] == "TRUE") waitForUserInput.Add(true);
                        else waitForUserInput.Add(false);

                        waitSeconds.Add(float.Parse(values[4]));

                        tutorialTargetingObject.Add(values[5]);
                    }
                  
                }
                else
                {
                    //headerNames = line;
                    header = false;
                }
                
            }
        }
        
        
        //******************************************************************************************
        private void PrepareRobotAndTextAndVariables()
        {
            Debug.Log("Starting tutorial phase " + introPhase);
            int index = IDs.FindIndex(a => a.Equals((int)introPhase));

            currentPhaseIntroRobotText = tutorialRobotTexts[index];
            waitTimer = waitSeconds[index];

            introText.SetText("");
         
            tutorialRobot = Instantiate(tutorialRobotPrefab, GameObject.Find("All").transform);
            tutorialRobot.transform.position = new Vector3(1.51f, -0.68f, 1.25f);
            tutorialRobot.transform.Rotate(-3.611f, -154.285f, 0);
            tutorialRobot.transform.localScale = new Vector3(5f, 5f, 5f);
            //tutorialRobot.SetActive(true);

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
            Destroy(portals);
            phaseStarted = false;
            txtBox.enabled = false;
            introText.SetText("");
        }
        
        
        private void ShowTableImage(GameObject canvas, Image img, Sprite sprite_img)
        {
            waitTimer -= Time.deltaTime;
            bool condition;
            condition = Input.GetMouseButtonDown(0) || waitTimer < 0.0f;
            if (!condition) return;
            tutorialRobot.SetActive(false);
            txtBox.enabled = false;
            introText.SetText("");
            
            canvas.SetActive(true);
            img.sprite = sprite_img;
            img.GetComponent<Image>().color = Color.white;

            if (introPhase == IntroPhase.One)
            {
                // ring = Instantiate(tutorialRingSectorsPrefab, new Vector3(-1.04f, 3.133f, 6.709461f),
                //     Quaternion.identity, GameObject.Find("All").transform);
                tutorialRing.SetActive(true);
            }

            showImg = false;
           
        }
        
        private void ShowPortals()
        {
            waitTimer2 -= Time.deltaTime;
            bool condition;
            condition = Input.GetMouseButtonDown(0) || waitTimer2 < 0.0f;
            if (!condition) return;
            txtBox.enabled = false;
            introText.SetText("");
            
            AudioSource[] tutorialRobotVoices;
            tutorialRobotVoices = tutorialRobot.GetComponents<AudioSource>();
            Random r = new Random();
            int rInt = r.Next(0, tutorialRobotVoices.Length);
            tutorialRobotVoices[rInt].Play();
            
            // tutorialRobot.transform.position = Vector3.MoveTowards(tutorialRobot.transform.position, new Vector3(4f, -0.68f, 2.7f),
            //                 Time.deltaTime * 0.5f); 
            
            portals = Instantiate(PortalsIntro, GameObject.Find("All").transform);
            GameObject  portal1 = portals.transform.Find("Portal1").gameObject;
            GameObject  portal2 = portals.transform.Find("Portal2").gameObject;
            GameObject  portal3 = portals.transform.Find("Portal3").gameObject;
            
            portal1.GetComponent<Renderer>().material.color = new Color(248/255f,255/255f,0f, 0.9f);
            portal2.GetComponent<Renderer>().material.color = new Color(198/255f,20/255f,2/255f,0.9f);
            portal3.GetComponent<Renderer>().material.color = new Color(112/255f,32/255f,154/255f,0.9f);
            
            portals.transform.position = new Vector3(0, 2, 5);
            
            showImg = false;
           
        }


        private void DestroyandExit(IntroPhase nextPhase, GameObject canvas)
        {
            waitTimer2 -= Time.deltaTime;
            bool condition2;
            condition2 = Input.GetMouseButtonDown(0) || waitTimer2 < 0.0f;
            if (!condition2) return;

            canvas.SetActive(false);
            
            introPhase = nextPhase;
            OnIntroPhaseChanged?.Invoke(introPhase);
            Destroy(tutorialRobot);
            phaseStarted = false;
            
        }

        private void MoveRing()
        {
            tutorialRing.transform.Rotate(0, 0, RotationZ*Time.deltaTime);
            tutorialRing.transform.position = tutorialRing.transform.position + 
                                                  new Vector3(0.0f, Mathf.Sin(Time.time*3f)/250f, 0.0f);
        }
    }
}
