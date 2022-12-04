using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using UnityEngine;
using TMPro;


namespace Assets.Scripts_FruitGame
{
    public class phase1Manager : MonoBehaviour
    {
        public static int FinalScore;
        private int NumSpawn = 10;
        
        public TextMeshProUGUI textObject;
            
        
        // Start is called before the first frame update
        void Start()
        {
            textObject = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            textObject.text = "Select the visible colour of the fruit";
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