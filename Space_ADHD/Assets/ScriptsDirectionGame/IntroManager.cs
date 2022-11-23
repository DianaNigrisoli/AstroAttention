using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using TMPro;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject tutorialRobot;

    private float tmpTutorialTime = 5.0f;
    private Boolean showingTutorial;
    
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
            tutorialCanvas.SetActive(true);
            tutorialText.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 1.30f, 0);
            tutorialText.text = "Tutorial...";
            
            tutorialRobot = Instantiate(tutorialRobot, GameObject.Find("All").transform);
            tutorialRobot.transform.Rotate(-3.611f, -154.285f, 0);
            tutorialRobot.transform.position = new Vector3(0.1408024f, 1.262115f, 0.7617905f);
            
            showingTutorial = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showingTutorial)
        {
            tmpTutorialTime -= Time.deltaTime;
            if (tmpTutorialTime <= 0.0f)
            {
                tutorialCanvas.SetActive(false);
                MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
                Destroy(tutorialRobot);
                Destroy(this);
            }
        }
    }
}
