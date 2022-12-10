using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts_FruitGame;

namespace Assets.Scripts_FruitGame
{
    public class WaitForNextFruitManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvasWaitForNext;
        [SerializeField] private Image countdownCircleTimer;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private GameObject canvasInstructions;
        public TextMeshProUGUI textInstructions;

        private float startTime = 3f;
        //private float startTime_skip = 5f;
        private float currentTime;
        private bool updateTime;
        private MiniGameStateFruit lastPlayedPhase;
        private List<MiniGameStateFruit> stateOrder = new List<MiniGameStateFruit>();
        private List<MiniGameStateFruit> stateOrder_skip = new List<MiniGameStateFruit>();
        private int stateIndex = 0;
        

        void Awake()
        {
            MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
            stateOrder.Add(MiniGameStateFruit.Instructions);
            stateOrder.Add(MiniGameStateFruit.ZeroTutorial);
            stateOrder.Add(MiniGameStateFruit.ZeroScene);
            stateOrder.Add(MiniGameStateFruit.Instructions);
            stateOrder.Add(MiniGameStateFruit.OneTutorial);
            stateOrder.Add(MiniGameStateFruit.OneScene);
            stateOrder.Add(MiniGameStateFruit.Instructions);
            stateOrder.Add(MiniGameStateFruit.TwoTutorial);
            stateOrder.Add(MiniGameStateFruit.TwoScene);
            stateOrder.Add(MiniGameStateFruit.Instructions);
            stateOrder.Add(MiniGameStateFruit.ThreeTutorial);
            stateOrder.Add(MiniGameStateFruit.ThreeScene);

            
            stateOrder_skip.Add(MiniGameStateFruit.ZeroScene);
            stateOrder_skip.Add(MiniGameStateFruit.OneScene);
            stateOrder_skip.Add(MiniGameStateFruit.TwoScene);
            stateOrder_skip.Add(MiniGameStateFruit.ThreeScene);


        }

        void OnDestroy()
        {
            MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
        }

        private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
        {
            if (state == MiniGameStateFruit.WaitForNext)
            {
                if (startTime == 0)
                {
                    canvasWaitForNext.SetActive(false);
                    canvasInstructions.SetActive(false);
                    currentTime = startTime;
                    updateTime = true;
                }
                else if (startTime >= 0)
                {
                    canvasWaitForNext.SetActive(true);
                    canvasInstructions.SetActive(true);
                    textInstructions = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();

                    // TODO: it seems that the built in canvas scaler works fine, eventually change this code
                    // countdownCircleTimer.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.33f, 0);
                    // countdownText.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.30f, 0);

                    currentTime = startTime;
                    countdownCircleTimer.fillAmount = 1.0f;
                    countdownText.text = (int)currentTime + "s";
                    updateTime = true;
                }
            }
            else if (state != MiniGameStateFruit.WaitForNext)
            {
                canvasWaitForNext.SetActive(false);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            canvasWaitForNext.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (updateTime)
            {
                currentTime -= Time.deltaTime;
                updateInstructions();
                if (currentTime <= 0.0f && !SkipTutorialButton.TutorialSkipped)
                {
                    // Stop the countdown timer              
                    updateTime = false;
                    currentTime = 0.0f;
                    canvasWaitForNext.SetActive(false);
                    updateMiniGameState_noSkip();
                    

                }
                
                else if (currentTime <= 0.0f && SkipTutorialButton.TutorialSkipped)
                {
                    // Stop the countdown timer              
                    updateTime = false;
                    currentTime = 0.0f;
                    canvasWaitForNext.SetActive(false);
                    updateMiniGameState_yesSkip();
                }

                countdownText.text = ((int)currentTime).ToString();
                float normalizedValue = Mathf.Clamp(
                    currentTime / startTime, 0.0f, 1.0f);
                countdownCircleTimer.fillAmount = normalizedValue;
            }
        
        }

        private void updateInstructions()
        {
            if (SkipTutorialButton.TutorialSkipped)
            {
                print(stateIndex);
                if ( stateOrder_skip[stateIndex] == MiniGameStateFruit.ZeroScene )
                    textInstructions.text = "Select the real colour of the fruit";
                if ( stateOrder_skip[stateIndex] == MiniGameStateFruit.OneScene )
                    textInstructions.text = "Select the visible colour of the fruit";
                if (stateOrder_skip[stateIndex] == MiniGameStateFruit.TwoScene)
                    textInstructions.text = "Select the real colour of the fruit";
                if (stateOrder_skip[stateIndex] == MiniGameStateFruit.ThreeScene)
                    textInstructions.text = "Depending on the visible colour, select the right fruit";
            }
            else
            {
                if ( stateOrder[stateIndex] == MiniGameStateFruit.Instructions )
                    canvasInstructions.SetActive(false);
                if ( stateOrder[stateIndex] == MiniGameStateFruit.ZeroScene )
                    textInstructions.text = "Select the real colour of the fruit";
                if ( stateOrder[stateIndex] == MiniGameStateFruit.OneScene )
                    textInstructions.text = "Select the visible colour of the fruit";
                if (stateOrder[stateIndex] == MiniGameStateFruit.TwoScene)
                    textInstructions.text = "Select the real colour of the fruit";
                if (stateOrder[stateIndex] == MiniGameStateFruit.ThreeScene)
                    textInstructions.text = "Depending on the visible colour, select the right fruit";
            }
           
        }
        private void updateMiniGameState_noSkip()
        {
            MiniGameManagerFruit.instance.UpdateMiniGameState(stateOrder[stateIndex]);
            if ((stateIndex-1) % 3 == 0)
                startTime = 4f;
            else startTime = 0f;
            stateIndex++;
        }

        private void updateMiniGameState_yesSkip()
        {
            MiniGameManagerFruit.instance.UpdateMiniGameState(stateOrder_skip[stateIndex]);
            startTime = 4f;
            stateIndex++;
            
        }
        
            
            
    }
        
        
        
        // private void updateMiniGameState()
        // {
        //     switch (lastPlayedPhase)
        //     {
        //         // case MiniGameStateFruit.Intro:
        //         //     MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.Instructions);
        //         //     lastPlayedPhase = MiniGameStateFruit.Instructions;
        //         //     break;
        //         case MiniGameStateFruit.Instructions:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.ZeroTutorial);
        //             lastPlayedPhase = MiniGameStateFruit.ZeroTutorial;
        //             break;
        //         case MiniGameStateFruit.ZeroTutorial:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.ZeroScene);
        //             lastPlayedPhase = MiniGameStateFruit.ZeroScene;
        //             break;
        //         case MiniGameStateFruit.ZeroScene:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.OneTutorial);
        //             lastPlayedPhase = MiniGameStateFruit.OneTutorial;
        //             break;
        //         case MiniGameStateFruit.OneTutorial:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.OneScene);
        //             lastPlayedPhase = MiniGameStateFruit.OneScene;
        //             break;
        //         case MiniGameStateFruit.OneScene:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.TwoTutorial);
        //             lastPlayedPhase = MiniGameStateFruit.TwoTutorial;
        //             break;
        //         case MiniGameStateFruit.TwoTutorial:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.TwoScene);
        //             lastPlayedPhase = MiniGameStateFruit.TwoScene;
        //             break;
        //         case MiniGameStateFruit.TwoScene:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.ThreeTutorial);
        //             lastPlayedPhase = MiniGameStateFruit.ThreeTutorial;
        //             break;
        //         case MiniGameStateFruit.ThreeTutorial:
        //             MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.ThreeScene);
        //             lastPlayedPhase = MiniGameStateFruit.ThreeScene;
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(lastPlayedPhase), lastPlayedPhase, null);
        //     }
        // }
}


