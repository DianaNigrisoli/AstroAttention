using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] GameObject musicManager;
    private GameObject currentPhaseManager;
    public static event Action<MiniGameState> OnMiniGameStateChanged;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(currentPhaseManager);
        UpdateMiniGameState(MiniGameState.Intro);
    }

    public void UpdateMiniGameState(MiniGameState newState)
    {
        this.state = newState;
        
        Debug.Log("Mini game state updated to: " + state.ToString("G"));
        Assets.ScriptsDirectionGame.phase0Manager.cases = 1000;
        Assets.ScriptsDirectionGame.phase1Manager.cases = 1000;
        Assets.ScriptsDirectionGame.phase2Manager.ROTcases = 1000;
        Assets.ScriptsDirectionGame.phase2Manager.SPTcases = 1000;
        Assets.ScriptsDirectionGame.phase3Manager.ROTcases = 1000;
        Assets.ScriptsDirectionGame.phase3Manager.SPTcases = 1000;
        
        switch (newState)
        {
            case MiniGameState.Intro:
                break;
            case MiniGameState.WaitForNext:
                break;
            case MiniGameState.End:
                break;
            case MiniGameState.Zero:
                currentPhaseManager = Instantiate(phase0Manager);
                musicManager.SetActive(true);
                break;
            case MiniGameState.One:
                Destroy(currentPhaseManager);
                currentPhaseManager = Instantiate(phase1Manager);
                break;
            case MiniGameState.Two:
                Destroy(currentPhaseManager);
                currentPhaseManager = Instantiate(phase2Manager);
                break;
            case MiniGameState.Three:
                Destroy(currentPhaseManager);
                currentPhaseManager = Instantiate(phase3Manager);
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
