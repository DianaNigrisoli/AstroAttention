using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using TMPro;
using UnityEngine;

namespace Assets.Scripts_FruitGame
{

    public class IntroManager : MonoBehaviour
    {
        // [SerializeField] private GameObject tutorialCanvas;
        // [SerializeField] private TextMeshProUGUI tutorialText;
        // [SerializeField] private GameObject tutorialRobot;
        // [SerializeField] private GameObject tutorialRing;
        //
        // private float tmpTutorialTime = 10.0f;
        // private Boolean showingTutorial;
        // private float rotationY = 30.0f;
        // private Vector3 tutorialRingStartPosition;

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
                    //per ora non fa nulla
            }
        }

        // Update is called once per frame
        void Update()
        {

            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            Destroy(this);

        }

    }
}
