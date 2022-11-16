using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    PortalSpawner portalSpawner;
    // Start is called before the first frame update
    void Start()
    {
        portalSpawner = GameObject.FindObjectOfType<PortalSpawner>(); 
    }

    private void OnTriggerExit(Collider other)
    {
        portalSpawner.SpawnPortal();
        Destroy(gameObject,1); //this will destroy the obj 2 seconds after trigger event
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
