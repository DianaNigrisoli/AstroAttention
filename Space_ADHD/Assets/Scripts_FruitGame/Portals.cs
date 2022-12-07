using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using Siccity.GLTFUtility;
using UnityEngine;

public class Portals : MonoBehaviour
{
    PortalSpawner portalSpawner;
    public static event Action<int> OnPortalSpawn;
    // Start is called before the first frame update
    void Start()
    {
        portalSpawner = GameObject.FindObjectOfType<PortalSpawner>(); 
    }

   private void OnTriggerExit(Collider other)
    {   
        Destroy(gameObject,5f); //this will destroy the obj
        portalSpawner.SpawnPortal();
        
        OnPortalSpawn?.Invoke(0); // if there is at least one subscriber invoke the function
        {
            
        }
        //print("Counter: " + FunctionTimer.counter);
        //print("React Time: "+ FunctionTimer.reactionTime);
    }
    
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        portalSpawner.SpawnPortal();
    }
    
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }
    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }
    
    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        if (state == MiniGameStateFruit.WaitForNext || state == MiniGameStateFruit.Intro || state == MiniGameStateFruit.Instructions)
        {
           gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
