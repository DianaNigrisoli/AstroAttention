using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using UnityEngine;
using TMPro;

namespace Assets.Scripts_FruitGame
{
    public class phase2Manager : MonoBehaviour
    {
        public static int FinalScore;
        private int NumSpawn = 3;
        private int currentPhase = 2;

        public TextMeshProUGUI textObject;
        private string ing_text = "Select the real colour of the fruit"; 
        private string ita_text = "Seleziona il colore reale della frutta";

        // Start is called before the first frame update
        void Start()
        {
            textObject = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.Language == "ENG") textObject.text = ing_text;
            else textObject.text = ita_text;
            if(PlayerMovement.PortalCounter == NumSpawn)
            {
                PlayerMovement.PortalCounter = 0;
                StartCoroutine(waiter());
            }
        }
        private double CalculateStdDev(List<float> values)
        {
            double avg = values.Sum()/values.Count;
            double sumOfSquares = 0;
            foreach(double value in values)
            {
                sumOfSquares += (value - avg) * (value - avg);
            }
            return Math.Sqrt(sumOfSquares / (values.Count -1 ));
            
        }

        private double CalculateFinalScore(List<float> timers, List<int> scores, float maxTimer)
        {
            double finalScore = 0;
            for (int i = 0; i < timers.Count; i++)
                finalScore = finalScore+ scores[i] * (maxTimer - timers[i]);
            return finalScore;
        }

        IEnumerator waiter()
        {
            yield return new WaitForSecondsRealtime(0.8f);
            PlayerMovement.reactionTimeMean[currentPhase] = PlayerMovement.ListReactionTime.Sum() / PlayerMovement.ListReactionTime.Count;
            PlayerMovement.reactionTimeStd[currentPhase] = CalculateStdDev(PlayerMovement.ListReactionTime);
            PlayerMovement.errorsFruitsG[currentPhase] = NumSpawn- PlayerMovement.score;
            PlayerMovement.kidScoreFruitG += CalculateFinalScore(PlayerMovement.ListReactionTime,
                PlayerMovement.ListScore, PlayerMovement.time1portal);
            Debug.Log("current phase: " + currentPhase);
            Debug.Log(PlayerMovement.reactionTimeMean[currentPhase]);
            Debug.Log(PlayerMovement.reactionTimeStd[currentPhase]);

            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            PlayerMovement.score = 0;
            PlayerMovement.ListReactionTime.Clear();
            textObject.text = "";
//            PlayerMovement.ListReactionTime = new List<float>(PlayerMovement.ListReactionTime.Count);
            Destroy(this);
        }
        
    }
}