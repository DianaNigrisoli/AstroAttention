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
        
        private string ing_text_real = "Select the real colour of the fruit"; 
        private string ita_text_real = "Seleziona il colore reale della frutta";
        private string ing_text_visib = "Select the visible colour of the fruit";
        private string ita_text_visib = "Seleziona il colore visibile della frutta";
        private string ing_text_fruit = "Choose the fruit that has its real color like the one on top";
        private string ita_text_fruit= "Scegli la frutta il cui colore reale è uguale al colore in alto";

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
                    canvasInstructions.SetActive(false);
                    canvasWaitForNext.SetActive(false);
                    
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
                if ( stateOrder_skip[stateIndex] == MiniGameStateFruit.ZeroScene )
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_real;
                    else textInstructions.text = ita_text_real;
                if ( stateOrder_skip[stateIndex] == MiniGameStateFruit.OneScene )
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_visib;
                    else textInstructions.text = ita_text_visib;
                if (stateOrder_skip[stateIndex] == MiniGameStateFruit.TwoScene)
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_real;
                    else textInstructions.text = ita_text_real;
                if (stateOrder_skip[stateIndex] == MiniGameStateFruit.ThreeScene)
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_fruit;
                    else textInstructions.text = ita_text_fruit;
            }
            else
            {
                if ( stateOrder[stateIndex] == MiniGameStateFruit.Instructions )
                    canvasInstructions.SetActive(false);
                if ( stateOrder[stateIndex] == MiniGameStateFruit.ZeroScene )
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_real;
                    else textInstructions.text = ita_text_real;
                if ( stateOrder[stateIndex] == MiniGameStateFruit.OneScene )
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_visib;
                    else textInstructions.text = ita_text_visib;
                if (stateOrder[stateIndex] == MiniGameStateFruit.TwoScene)
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_real;
                    else textInstructions.text = ita_text_real;
                if (stateOrder[stateIndex] == MiniGameStateFruit.ThreeScene)
                    if (GameManager.instance.Language == "ENG") textInstructions.text = ing_text_fruit;
                    else textInstructions.text = ita_text_fruit;
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
        
    
}


