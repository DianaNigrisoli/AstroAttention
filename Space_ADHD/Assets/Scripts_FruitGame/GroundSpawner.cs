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
       GameObject temp = Instantiate(groundTile, NextSpawnPoint, Quaternion.identity);
       NextSpawnPoint = temp.transform.GetChild(0).transform.position; 
   }
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        { 
            SpawnTile();
        }
        
    }

   
}
