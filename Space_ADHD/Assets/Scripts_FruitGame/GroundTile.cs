using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>(); 
        //la funzione trova tutti gli oggetti di tipo groundspawner, quindi se ne abbiamo più di uno, non va bene
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject,2); //this will destroy the obj 2 seconds after trigger event
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
