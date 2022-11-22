using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance = null;
    public MiniGameState state;
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

        switch (newState)
        {
            case MiniGameState.Intro:
                HandleIntro();
                break;
            case MiniGameState.WaitForNext:
                break;
            case MiniGameState.End:
                break;
            case MiniGameState.Zero:
                break;
            case MiniGameState.One:
                break;
            case MiniGameState.Two:
                break;
            case MiniGameState.Three:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnMiniGameStateChanged?.Invoke(newState); // if there is at least one subscriber invoke the function
        {
            
        }
    }

    private void HandleIntro()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
