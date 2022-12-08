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
        
        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            //TODO: bisogna cambiare l'elenco delle fasi per saltare anche le varie intro al gioco
            
            GameObject[] IntroFruitObj;
            IntroFruitObj = GameObject.FindGameObjectsWithTag("IntroFruit");
            
            foreach (var x in IntroFruitObj)
            {
                Destroy(x);
            }
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}