using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using UnityEngine.SceneManagement;
public class MiniGameManagerFruit : MonoBehaviour
{
    public static MiniGameManagerFruit instance = null;
    public MiniGameStateFruit state;
    public GameObject phase0Manager;
    public GameObject phase1Manager;
    public GameObject phase2Manager;
    public GameObject phase3Manager;
    public GameObject phase0TutorialManager;
    public GameObject phase1TutorialManager;
    public GameObject phase2TutorialManager;
    public GameObject phase3TutorialManager;

    public static event Action<MiniGameStateFruit> OnMiniGameStateChanged;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateMiniGameState(MiniGameStateFruit.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMiniGameState(MiniGameStateFruit newState)
    {
        this.state = newState;
        
        Debug.Log("Mini game state updated to: " + state.ToString("G"));

        switch (newState)
        {
            case MiniGameStateFruit.Intro:
                break;
            case MiniGameStateFruit.WaitForNext:
                break;
            case MiniGameStateFruit.End:
                break;
            case MiniGameStateFruit.Instructions:
                break;
            case MiniGameStateFruit.ZeroTutorial:
                Instantiate(phase0TutorialManager);
                break;
            case MiniGameStateFruit.ZeroScene:
                Instantiate(phase0Manager);
                break;
            case MiniGameStateFruit.OneTutorial:
                Instantiate(phase1TutorialManager);
                break;
            case MiniGameStateFruit.OneScene:
                Instantiate(phase1Manager);
                break;
            case MiniGameStateFruit.TwoTutorial:
                Instantiate(phase2TutorialManager);
                break;
            case MiniGameStateFruit.TwoScene:
                Instantiate(phase2Manager);
                break;
            case MiniGameStateFruit.ThreeTutorial:
                Instantiate(phase3TutorialManager);
                break;
            case MiniGameStateFruit.ThreeScene:
                Instantiate(phase3Manager);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnMiniGameStateChanged?.Invoke(newState); // if there is at least one subscriber invoke the function
        {
            
        }

    }
}


