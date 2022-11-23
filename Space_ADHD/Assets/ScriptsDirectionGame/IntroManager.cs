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
    [SerializeField] private GameObject tutorialRing;

    private float tmpTutorialTime = 10.0f;
    private Boolean showingTutorial;
    private float rotationY = 30.0f;
    private Vector3 tutorialRingStartPosition;
    
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

            tutorialRing = Instantiate(tutorialRing, GameObject.Find("All").transform);
            tutorialRing.transform.position = new Vector3(-0.094f, 1.379f, 1.127f);
            tutorialRing.transform.Rotate(-7.772f, 0, 0);
            tutorialRing.transform.localScale = new Vector3(0.17558f, 0.17558f, 0.17558f);
            tutorialRingStartPosition = tutorialRing.transform.position;
            
            showingTutorial = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showingTutorial)
        {
            
            tutorialRing.transform.Rotate(0, rotationY*Time.deltaTime, 0);
            tutorialRing.transform.position = tutorialRingStartPosition + new Vector3(0.0f, Mathf.Sin(Time.time*3f)/250f, 0.0f);
            tmpTutorialTime -= Time.deltaTime;
            if (tmpTutorialTime <= 0.0f)
            {
                tutorialCanvas.SetActive(false);
                MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
                Destroy(tutorialRing);
                Destroy(tutorialRobot);
                Destroy(this);
            }
        }
    }
}
