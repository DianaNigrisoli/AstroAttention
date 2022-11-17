using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonAction : MonoBehaviour
{
    public static String selectedPlanet;
    private Boolean selected = false;
    private float speed = 1.0f;
    private float spaceshipScalingFactor = -0.37f;
    private Vector3 targetPositionCamera;
    private Vector3 targetPositionSpaceship;
    private GameObject spaceship;

    public void buttonMethod()
    {
        Debug.Log("Done SIUIIIII");
        GameObject planet = GameObject.Find(selectedPlanet);
        targetPositionCamera = planet.transform.position + new Vector3(0.0f, 0.0f, 3.08f);
        targetPositionSpaceship = planet.transform.position + new Vector3(-0.04f, 0.3f, 0.0f);;
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
            var step = speed * Time.deltaTime * 2; // calculate distance to move
            Camera.main.transform.position =
                Vector3.MoveTowards(Camera.main.transform.position, targetPositionCamera, step * 0.97f);
            spaceship.transform.position =
                Vector3.MoveTowards(spaceship.transform.position, targetPositionSpaceship, step * 1.37f);
            spaceship.transform.localScale += new Vector3(spaceshipScalingFactor * Time.deltaTime,
                spaceshipScalingFactor* Time.deltaTime, spaceshipScalingFactor * Time.deltaTime);
        }

        if (Camera.main.transform.position == targetPositionCamera)
        {
            selected = false;
            Destroy(spaceship);
            changeScene();
        }
    }

    private void changeScene()
    {
        Debug.Log("ChangingScene");
        return; //TODO: uncomment to change scene
        if (selectedPlanet == "planet1Button")
        {
            SceneManager.LoadScene("Scenes/FruitGame");
        }
    }
}