using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts_A_General;
using UnityEngine;

namespace Assets.Scripts_FruitGame
{
    public class phase2Manager : MonoBehaviour
    {
        public static int FinalScore;
        private int NumSpawn = 10;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(PlayerMovement.PortalCounter == NumSpawn)
            {
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
}