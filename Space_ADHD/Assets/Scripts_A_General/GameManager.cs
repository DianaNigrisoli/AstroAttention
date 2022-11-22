using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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

    // Update is called once per frame
    void Update()
    {

    }
}
