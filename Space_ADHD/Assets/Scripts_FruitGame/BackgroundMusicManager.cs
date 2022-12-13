using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_FruitGame;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource sound; 
    
    // Start is called before the first frame update
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }

    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.Intro || state == MiniGameStateFruit.Instructions || state == MiniGameStateFruit.WaitForNext)
            sound.Stop();
        else
        {
            sound.Play();
        }
    }
}

