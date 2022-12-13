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