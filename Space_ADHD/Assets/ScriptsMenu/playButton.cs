using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playButton : MonoBehaviour
{
    // Start is called before the first frame update
    private String selectedPlanet;
    private GameObject king;
    private Vector3 targetPosition;
    private bool moveKingBool;

    void Start()
    {
        this.transform.position = new Vector3(Screen.width / 2, Screen.height / 6, 50);
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        this.moveKingBool = false;
        gameObject.SetActive(false);
    }

    private void TaskOnClick()
    {
        selectedPlanet = buttonAction.selectedPlanet;
        Debug.Log("Starting Intro");
        this.transform.position = new Vector3(Screen.width*2, Screen.height*2, 50);
        miniGameIntro();
    }

    private void miniGameIntro()
    {
        switch (selectedPlanet)
        {
            case "planet1Button":
                Debug.Log("Scene planet 1");
                GameObject king1 = GameObject.Find("kingPlanet1");
                Vector3 targetPosition1 = new Vector3(-1.56f, 1.98f, 1.21f);
                moveKing(king1, targetPosition1);
                break;
            default:
                Debug.Log("GG");
                break;

        }
    }

    private void moveKing(GameObject king, Vector3 targetPosition)
    {
        this.king = king;
        this.targetPosition = targetPosition;
        this.moveKingBool = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveKingBool)
        {
            var step = 5.0f * Time.deltaTime;
            king.transform.position = Vector3.MoveTowards(king.transform.position, targetPosition, step);
            
        }

        if (moveKingBool && king.transform.position == targetPosition)
        {
            moveKingBool = false;
        }
    }
}
