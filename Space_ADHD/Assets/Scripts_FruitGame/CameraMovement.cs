using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    
    private Vector3 offset = new Vector3(0, 4, -11);
    
    // Update is called once per frame
    void Update()
    {
        //transform.localPosition.z =  player.transform.localPosition.z + offset.z;
        
        Vector3 position = transform.position ;
        position.z = (player.transform.localPosition + offset).z;
        transform.position = position ;
    }
}
