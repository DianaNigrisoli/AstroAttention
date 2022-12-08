using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts_FruitGame
{
    public class WaitForNextFruitManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvasWaitForNext;
        [SerializeField] private Image countdownCircleTimer;
        [SerializeField] private TextMeshProUGUI countdownText;

        private float startTime = 0.0f;
        private float currentTime;
        private bool updateTime;
        private MiniGameStateFruit lastPlayedPhase;
        private List<MiniGameStateFruit> stateOrder = new List<MiniGameStateFruit>();
        private int stateIndex = 0;
        

        void Awake()
        {
            MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
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
                    currentTime = startTime;
                    updateTime = true;
                }
                else if (startTime >= 0)
                {
                    canvasWaitForNext.SetActive(true);

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
                if (currentTime <= 0.0f)
                {
                    // Stop the countdown timer              
                    updateTime = false;
                    currentTime = 0.0f;
                    canvasWaitForNext.SetActive(false);
                    updateMiniGameState();

                }

                countdownText.text = ((int)currentTime).ToString();
                float normalizedValue = Mathf.Clamp(
                    currentTime / startTime, 0.0f, 1.0f);
                countdownCircleTimer.fillAmount = normalizedValue;
            }
        
        }

        void updateMiniGameState()
        {
            MiniGameManagerFruit.instance.UpdateMiniGameState(stateOrder[stateIndex]);
            if ((stateIndex) % 3 == 0)
                startTime = 3f;
            else startTime = 0f;
            stateIndex++;
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

}
