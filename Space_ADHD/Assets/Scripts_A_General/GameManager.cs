using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private String playerName;
    
    void Awake()
    {
        gameObject.tag = "GameManager";
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        playerName = "Mario";
        DontDestroyOnLoad(this);    
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 100 == 0)
        {
            Debug.Log(playerName);
        }
    }
}

public enum MenuState
{
    ProfileSelection,
    Map
}

public enum MiniGame
{
    FruitGame,
    DirectionGame
}
