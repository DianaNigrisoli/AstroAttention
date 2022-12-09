using System;
using Assets.Scripts_A_General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts_FruitGame
{
    public class SkipTutorialButton : MonoBehaviour
    {
        public static Boolean TutorialSkipped = false;
        
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
            gameObject.SetActive(state == MiniGameStateFruit.Intro);
        }

        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            instructionsManager.showingTutorial = false;
            IntroManager.showingTutorial = false;
            TutorialSkipped = true;
            // GameObject[] IntroFruitObj;
            // IntroFruitObj = GameObject.FindGameObjectsWithTag("IntroFruit");
            //
            // foreach (var x in IntroFruitObj)
            // {
            //     x.SetActive(false);
            // }
            
            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}