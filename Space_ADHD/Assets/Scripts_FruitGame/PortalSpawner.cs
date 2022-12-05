using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject Portals;
    
    private Vector3 NextSpawnOffset = new Vector3(x: 0, y: 2, z: 30);
    // Start is called before the first frame update
    
    public void SpawnPortal()
    {
        GameObject temp = Instantiate(Portals, NextSpawnOffset, Quaternion.identity, GameObject.Find("All").transform);
        NextSpawnOffset.z += 50;
    }
    // Start is called before the first frame update
    private void Start()
    {
        SpawnPortal();
    }
    
    
}
