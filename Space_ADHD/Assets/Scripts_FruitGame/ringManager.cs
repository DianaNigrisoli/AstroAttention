using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_FruitGame;
using UnityEngine;

public class ringManager : MonoBehaviour
{
    private Boolean spin;
    private const float RotationZ = 30.0f;
    
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }

    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }
    // Start is called before the first frame update

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        
        if (state == MiniGameStateFruit.Intro || state == MiniGameStateFruit.Instructions || state == MiniGameStateFruit.WaitForNext)
            return;
        else
        {
            spin = true;
        }
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
            MoveRing();
            
    }
    
    private void MoveRing()
    {
        gameObject.transform.Rotate(0, 0, RotationZ*Time.deltaTime);
        gameObject.transform.position = gameObject.transform.position + 
                                          new Vector3(0.0f, Mathf.Sin(Time.time*3f)/250f, 0.0f);
    }
}
