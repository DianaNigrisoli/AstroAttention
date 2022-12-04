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

        public TextMeshProUGUI textObject;
        // Start is called before the first frame update
        void Start()
        {
            textObject = GameObject.Find("TextIndication").GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            textObject.text = "Depending on the visible colour, select the right fruit";
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
                
            MiniGameManager.instance.UpdateMiniGameState(MiniGameState.End);
            PlayerMovement.PortalCounter = 0;
            PlayerMovement.score = 0;
            Destroy(this);
        }
    }

}