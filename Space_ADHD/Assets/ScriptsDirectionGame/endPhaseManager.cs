using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts_A_General;
using Assets.ScriptsDirectionGame;

    public class endPhaseManager : MonoBehaviour
    {
		[SerializeField] GameObject endCanvas;
        [SerializeField] GameObject introCanvas;
        [SerializeField] GameObject wfnCanvas;
		[SerializeField] TextMeshProUGUI textObject;
		[SerializeField] GameObject spaceship;
        [SerializeField] GameObject hLasers;
        [SerializeField] GameObject vLasers;
        [SerializeField] GameObject cockpit;
        private MiniGameState lastPlayedPhase;
        
        [SerializeField] TextMeshProUGUI score_title;
        [SerializeField] TextMeshProUGUI question1;
        [SerializeField] TextMeshProUGUI question2;
        private string ing_score_t = "SCORE";
        private string ing_text_q1 = "If I had this game on my iPad, I think I would like to play it a lot";
        private string ing_text_q2 = "I was proud of how I played";   
   
        private string ita_score_t = "PUNTEGGIO";
        private string ita_text_q1 = "Se avessi questo gioco sul mio iPad, penso mi piacerebbe giocarci molto"; 
        private string ita_text_q2 = "Sono entusiasta di come ho giocato"; 

        void Awake()
        {
            MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged; // subscription to state change of MiniGameManager
            lastPlayedPhase = MiniGameState.Three;
        }

        void OnDestroy()
        {
            MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
        }

        private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
        {
            if (state == MiniGameState.End)
            {
                endCanvas.SetActive(true);
                if (GameManager.instance.Language == "ENG")
                {
	                score_title.text = ing_score_t;
	                question1.text = ing_text_q1;
	                question2.text = ing_text_q2;
                }
                else
                {
	                score_title.text = ita_score_t;
	                question1.text = ita_text_q1;
	                question2.text = ita_text_q2;
                }
	            introCanvas.SetActive(false);
    	        wfnCanvas.SetActive(false);
				string score = string.Format("{0:N2}", CannonBehavior.kidScoreDirectionG);
				textObject.text = score;
				Destroy(spaceship);
				Destroy(cockpit);
				Destroy(hLasers);
				Destroy(vLasers);
            }
            else
            {
                endCanvas.SetActive(false);
            }
        }
    
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }