using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Assets.Scripts_A_General;

namespace Assets.Scripts_FruitGame
{
    public class phase3Manager : MonoBehaviour
    {
        public static int FinalScore;
        private int NumSpawn = 10;
        private int currentPhase = 3;
        private string ing_text = "Choose the fruit that has its real color like the one on top";
        private string ita_text = "Scegli la frutta il cui colore reale è uguale al colore in alto";

        public TextMeshProUGUI textObject;
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
            PlayerMovement.errorsFruitsG[currentPhase] = 10 - PlayerMovement.score;
            PlayerMovement.kidScoreFruitG += (int)CalculateFinalScore(PlayerMovement.ListReactionTime,
                PlayerMovement.ListScore, PlayerMovement.time1portal);
            
            //TODO: FINAL SCORE DA CAMBIARE!!!   
                
         
            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.End);
            PlayerMovement.PortalCounter = 0;
            PlayerMovement.score = 0;
            textObject.text = "";
//            PlayerMovement.ListReactionTime = new List<float>(PlayerMovement.ListReactionTime.Count);
            Destroy(this);
        }
    }

}