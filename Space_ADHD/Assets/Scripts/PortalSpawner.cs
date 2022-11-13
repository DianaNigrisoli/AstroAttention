using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject Portals;
    
    private Vector3 NextSpawnOffset = new Vector3(x: 0, y: 2, z: 30);
    // Start is called before the first frame update
    public void SpawnPortal()
    {
        GameObject temp = Instantiate(Portals, NextSpawnOffset, Quaternion.identity);
        NextSpawnOffset.z += 100;
    }
    // Start is called before the first frame update
    private void Start()
    {
        SpawnPortal();
    }
    
}
