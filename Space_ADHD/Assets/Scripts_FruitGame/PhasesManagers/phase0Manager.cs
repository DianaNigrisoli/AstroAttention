using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Scripts_FruitGame
{
 
 
    public class phase0Manager : MonoBehaviour
    {
        public static int FinalScore;
        private int NumSpawn = 10;
        private int currentPhase = 0;
        
        public TextMeshProUGUI textObject; 

        //Cosa fa questo script: 
        //Conto del punteggio considerando tempo di reazione (calcolato FunctionTimer.cs) e portali corretti (calcolato in PlayerMovement.cs)
        //Tiene conto di numero di portali spawnati (contati nel player movement) e triggera l'inizio della fase successiva
        
        // Start is called before the first frame update
        void Start()
        {
            textObject = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            textObject.text = "Select the real colour of the fruit";
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
                finalScore = finalScore + scores[i] * (maxTimer - timers[i]);
            return finalScore;
        }
        IEnumerator waiter()
        {
            yield return new WaitForSecondsRealtime(0.8f);
                //TODO: FINAL SCORE DA CAMBIARE!!!   
            // In ogni phase manager calcolare elemento iesimo delle liste:
            //     Mean
            //     Std
            //     errors
            
            // prove
            
            PlayerMovement.reactionTimeMean[currentPhase] = PlayerMovement.ListReactionTime.Sum() / PlayerMovement.ListReactionTime.Count;
            PlayerMovement.reactionTimeStd[currentPhase] = CalculateStdDev(PlayerMovement.ListReactionTime);
            PlayerMovement.errorsFruitsG[currentPhase] = 10 - PlayerMovement.score;
            PlayerMovement.kidScoreFruitG = (int)CalculateFinalScore(PlayerMovement.ListReactionTime,
                PlayerMovement.ListScore, PlayerMovement.time1portal);
            
            MiniGameManagerFruit.instance.UpdateMiniGameState(MiniGameStateFruit.WaitForNext);
            PlayerMovement.PortalCounter = 0;
            PlayerMovement.score = 0;
            PlayerMovement.ListReactionTime.Clear();
//            PlayerMovement.ListReactionTime = new List<float>(PlayerMovement.ListReactionTime.Count);
            Destroy(this);
        }
    }
}
