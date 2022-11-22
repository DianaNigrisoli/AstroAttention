using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject button;

    void Awake()
    {
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged; // subscription to state change of MiniGameManager
    }

    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged; // unsubscription to state change of MiniGameManager
    }

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        introPanel.SetActive(state == MiniGameState.Intro);
    }

    void Start()
    {
        introPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
