using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameState state;
    private string currentUserId;
    public static event Action<GameState> OnGameStateChanged;
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        Debug.Log("Game state updated to: " + newState.ToString("G"));

        switch (newState)
        {
            case GameState.UserSelection:
                break;
            case GameState.Map:
                break;
            case GameState.Settings:
                break;
            case GameState.FruitGame:
                break;
            case GameState.DirectionGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }

    void Start()
    {
        UpdateGameState(GameState.UserSelection);
    }

    void Awake()
    {
        gameObject.tag = "GameManager";
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);    
    }

    public string CurrentUserId
    {
        get
        {
            if (String.IsNullOrEmpty(currentUserId))
            {
                throw new ArgumentNullException();
                //TODO: it's just for debug... 
            }
            return currentUserId;
        }
        set => currentUserId = value;
    }
    
}
