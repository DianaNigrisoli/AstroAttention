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
    private string language;
    [SerializeField] private GameObject loadingScreen;
    
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
                GameObject temp1 = Instantiate(loadingScreen);
                loadingScreen.gameObject.SetActive(true);
                break;
            case GameState.DirectionGame:
                GameObject temp2 = Instantiate(loadingScreen);
                loadingScreen.gameObject.SetActive(true);
                break;
            case GameState.DoctorInterface:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    void Start()
    {
        language = PlayerPrefs.GetString("language");
        if (String.IsNullOrEmpty(language))
        {
            Language = "ENG";
        }
        if(instance == this)
            UpdateGameState(GameState.UserSelection);
    }

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != this){
            Destroy(this.gameObject);
        }
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
    
    public string Language
    {
        get => language;
        set
        {
            PlayerPrefs.SetString("language", value);
            language = value;
        }
    }

    public GameState State => state;
}
