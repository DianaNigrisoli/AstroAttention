using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;
using Assets.ScriptsDirectionGame;

public class musicManager : MonoBehaviour
{
    static public AudioSource sound; 
    
    // Start is called before the first frame update
    void Awake()
    {
        MiniGameManager.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
        sound = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        MiniGameManager.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }

    private void Start()
    {

    }

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameState state)
    {
        if (state == MiniGameState.Zero || state == MiniGameState.One || state == MiniGameState.Two || state==MiniGameState.Three)
            {sound.Play();}
        else
        {sound.Stop();}
    }
}
