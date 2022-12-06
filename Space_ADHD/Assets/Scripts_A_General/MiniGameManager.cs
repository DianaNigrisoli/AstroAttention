using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance = null;
    public MiniGameState state;
    [SerializeField] GameObject phase0Manager;
    [SerializeField] GameObject phase1Manager;
    [SerializeField] GameObject phase2Manager;
    [SerializeField] GameObject phase3Manager;
    //[SerializeField] GameObject endPhaseManager;
    public static event Action<MiniGameState> OnMiniGameStateChanged;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateMiniGameState(MiniGameState.Intro);
    }

    public void UpdateMiniGameState(MiniGameState newState)
    {
        this.state = newState;
        
        Debug.Log("Mini game state updated to: " + state.ToString("G"));
        
        switch (newState)
        {
            case MiniGameState.Intro:
                break;
            case MiniGameState.WaitForNext:
                break;
            case MiniGameState.End:
                break;
            case MiniGameState.Zero:
                Instantiate(phase0Manager);
                break;
            case MiniGameState.One:
                Instantiate(phase1Manager);
                break;
            case MiniGameState.Two:
                Instantiate(phase2Manager);
                break;
            case MiniGameState.Three:
                Instantiate(phase3Manager);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnMiniGameStateChanged?.Invoke(newState); // if there is at least one subscriber invoke the function
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
