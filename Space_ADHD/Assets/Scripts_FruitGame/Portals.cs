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
        Destroy(gameObject,1); //this will destroy the obj
        portalSpawner.SpawnPortal();
        //StartCoroutine(waiter());
    }
    
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        portalSpawner.SpawnPortal();
    }
}
