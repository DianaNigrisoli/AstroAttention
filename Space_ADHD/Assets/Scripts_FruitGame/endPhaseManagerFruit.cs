using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;

public class endPhaseManagerFruit : MonoBehaviour
{
    [SerializeField] GameObject endCanvas;
    [SerializeField] GameObject introCanvas;
    [SerializeField] GameObject wfnCanvas;
    [SerializeField] GameObject fruitCanvas;
    [SerializeField] GameObject textCanvas;
    [SerializeField] GameObject correctAnsCanvas;
    [SerializeField] GameObject player;

    
    
    [SerializeField] TextMeshProUGUI textObject;
    private MiniGameStateFruit lastPlayedPhase;
    
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged; // subscription to state change of MiniGameManager
        lastPlayedPhase = MiniGameStateFruit.ThreeScene;
    }

    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
    }
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.End)
        {
            endCanvas.SetActive(true);
            introCanvas.SetActive(false);
            wfnCanvas.SetActive(false);
            fruitCanvas.SetActive(false);
            textCanvas.SetActive(false);
            correctAnsCanvas.SetActive(false);
            player.SetActive(false);
            
            string score = string.Format("{0:N2}", PlayerMovement.kidScoreFruitG );
            textObject.text = score;
        }
        else
        {
            endCanvas.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
