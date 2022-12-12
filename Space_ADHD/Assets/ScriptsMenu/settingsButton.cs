using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.UI;

public class settingsButton : MonoBehaviour
{

    private GameState gameState;
    private GameState backToState;
    
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        if(gameState is GameState.DirectionGame or GameState.FruitGame) return;

        if (gameState == GameState.Settings)
        {
            GameManager.instance.UpdateGameState(backToState);
        }
        else
        {
            backToState = gameState;
            GameManager.instance.UpdateGameState(GameState.Settings);
        }
    }

    private void GameManagerOnOnGameStateChanged(GameState newState)
    {
        gameState = newState;
    }
    
}
