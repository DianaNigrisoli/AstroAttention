using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts_A_General
{
    public class WaitForNextManager : MonoBehaviour
    {
        [SerializeField] private GameObject introCanvas;
        [SerializeField] private Image countdownCircleTimer;
        [SerializeField] private TextMeshProUGUI countdownText;

        private float startTime = 5.0f;
        private float currentTime;
        private bool updateTime;
        private MiniGameState lastPlayedPhase;
        void Awake()
        {
            MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged; // subscription to state change of MiniGameManager
            lastPlayedPhase = MiniGameState.Intro;
        }

        void OnDestroy()
        {
            MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
        }

        private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
        {
            if (state == MiniGameState.WaitForNext)
            {
                introCanvas.SetActive(true);
                
                // TODO: it seems that the built in canvas scaler works fine, eventually change this code
                // countdownCircleTimer.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.33f, 0);
                // countdownText.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.30f, 0);
                
                currentTime = startTime;
                countdownCircleTimer.fillAmount = 1.0f;
                countdownText.text = (int)currentTime + "s";
                updateTime = true;
            }
            else
            {
                introCanvas.SetActive(false);
            }
        }

        void Start()
        {
            introCanvas.SetActive(false);
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
                    introCanvas.SetActive(false);
                    updateMiniGameState();
                    
                }
                countdownText.text = (int)currentTime + "s";
                float normalizedValue = Mathf.Clamp(
                    currentTime /startTime, 0.0f, 1.0f);
                countdownCircleTimer.fillAmount = normalizedValue;
            }
        }

        private void updateMiniGameState()
        {
            switch (lastPlayedPhase)
            {
                case MiniGameState.Intro:
                    MiniGameManager.instance.UpdateMiniGameState(MiniGameState.Zero);
                    lastPlayedPhase = MiniGameState.Zero;
                    break;
                case MiniGameState.Zero:
                    MiniGameManager.instance.UpdateMiniGameState(MiniGameState.One);
                    lastPlayedPhase = MiniGameState.One;
                    break;
                case MiniGameState.One:
                    MiniGameManager.instance.UpdateMiniGameState(MiniGameState.Two);
                    lastPlayedPhase = MiniGameState.Two;
                    break;
                case MiniGameState.Two:
                    MiniGameManager.instance.UpdateMiniGameState(MiniGameState.Three);
                    lastPlayedPhase = MiniGameState.Three;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lastPlayedPhase), lastPlayedPhase, null);
            }
        }
    }
}
