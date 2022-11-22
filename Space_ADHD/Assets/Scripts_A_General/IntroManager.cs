using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private TextMeshProUGUI countdownText;

    private float startTime = 5.0f;
    private float currentTime;
    private bool updateTime;
    void Awake()
    {
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged; // subscription to state change of MiniGameManager
    }

    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
    }

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.Intro)
        {
            introCanvas.SetActive(true);
            countdownCircleTimer.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.33f, 0);
            countdownText.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.33f, 0);
            currentTime = startTime;
            countdownCircleTimer.fillAmount = 1.0f;
            countdownText.text = (int)currentTime + "s";
            updateTime = true;
            Debug.Log("HERE");
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
                MiniGameManager.instance.UpdateMiniGameState(MiniGameState.Zero);
            }
            countdownText.text = (int)currentTime + "s";
            float normalizedValue = Mathf.Clamp(
                currentTime /startTime, 0.0f, 1.0f);
            countdownCircleTimer.fillAmount = normalizedValue;
        }
    }
}
