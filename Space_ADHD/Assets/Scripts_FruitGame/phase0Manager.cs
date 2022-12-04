using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts_FruitGame
{
 
 
    public class phase0Manager : MonoBehaviour
    {
        public static int FinalScore;
        private int NumSpawn = 10;
        
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
            textObject.text = "Select the semantic colour of the fruit";
            if(PlayerMovement.PortalCounter == NumSpawn)
            { 
                StartCoroutine(waiter());
            }
            
        }

        IEnumerator waiter()
        {
            yield return new WaitForSecondsRealtime(0.8f);
            FinalScore = PlayerMovement.score + (int)PlayerMovement.ListReactionTime.Sum();
            
            //TODO: FINAL SCORE DA CAMBIARE!!!   
            
            print("Final Score: "+ FinalScore);
            
            MiniGameManager.instance.UpdateMiniGameState(MiniGameState.WaitForNext);
            PlayerMovement.PortalCounter = 0;
            PlayerMovement.score = 0;
            Destroy(this);
        }
    }
}
