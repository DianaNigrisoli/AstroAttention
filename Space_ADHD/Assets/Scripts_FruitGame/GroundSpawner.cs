using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundSpawner : MonoBehaviour
{   
   public GameObject groundTile;
   Vector3 NextSpawnPoint;

   public void SpawnTile()
   {
       for (int i = 0; i < 8; i++)
       { 
           GameObject temp = Instantiate(groundTile, NextSpawnPoint, Quaternion.identity, GameObject.Find("All").transform);
           NextSpawnPoint = temp.transform.GetChild(0).transform.position;  
       }
   }
    // Start is called before the first frame update
    private void Start()
    {
        SpawnTile();
    }

   
}
