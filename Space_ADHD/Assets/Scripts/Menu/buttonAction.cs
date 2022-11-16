using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonAction : MonoBehaviour
{
    private Boolean selected = false;
    public float speed = 1.0f;
    public Vector3 targetPosition = new Vector3(-1.22f, 3.42f, 3.1f);
    private GameObject spaceship;
    
    public void buttonMethod(){
        Debug.Log("Done SIUIIIII");
        selected = true;
        
    }

    public void Awake()
    {
        spaceship = GameObject.Find("spaceshipMenu");
    }

    public void Update()
    {
        if (selected)
        {
            var step =  speed * Time.deltaTime * 2; // calculate distance to move
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targetPosition, step);
            spaceship.transform.position = Vector3.MoveTowards(spaceship.transform.position, targetPosition, step * 1.3f);
        }

        if (Camera.main.transform.position == targetPosition)
        {
            changeScene();
        }
    }

    private void changeScene()
    {
        // SceneManager.LoadScene("Scenes/SampleScene");
    }
}
