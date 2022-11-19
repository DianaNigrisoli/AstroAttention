using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playButton : MonoBehaviour
{
    // Start is called before the first frame update
    private String selectedPlanet;
    private GameObject king;
    private Vector3 targetPosition;
    private bool moveKingBool;
	private bool kingHasSpoken = false;
    public TextMeshProUGUI dialog;
	public RawImage txtBox;

    void Start()
    {
        this.transform.position = new Vector3(Screen.width / 2, Screen.height / 6, 50);
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        this.moveKingBool = false;
        gameObject.SetActive(false);
		txtBox.enabled = false;
    }

    private void TaskOnClick()
    {
        selectedPlanet = buttonAction.selectedPlanet;
        Debug.Log("Starting Intro");
		Debug.Log(selectedPlanet);
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
                Vector3 targetPosition1 = new Vector3(-2f, 1.98f, 1.77f);
                moveKing(king1, targetPosition1);
				searchInCSV("Planet 1");
				kingHasSpoken = true;
                break;
            case "planet2Button":
                Debug.Log("Scene planet 2");
                GameObject king2 = GameObject.Find("kingPlanet2");
                Vector3 targetPosition2 = new Vector3(2.15f, 1.23f, 1.73f);
                moveKing(king2, targetPosition2);
				searchInCSV("Planet 2");
				kingHasSpoken = true;
                break;
            default:
                Debug.Log("GG");
                break;
        }
    }

	private void searchInCSV(string keyword)
	{
		List<string> listA = new List<string>();
        List<string> listB = new List<string>();
        using (var reader = new StreamReader(@"Assets/Resources/dialogs.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                Debug.Log(line);
                listA.Add(values[0]);
                listB.Add(values[1]);
                Debug.Log(values[0]);
                Debug.Log(values[1]);
            }
        }
        int index = listA.FindIndex(a => a.Contains(keyword));
        dialog.SetText(listB[index]);
		txtBox.enabled = true;
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
		if (kingHasSpoken && Input.GetMouseButtonDown(0))
		{		
			switch (selectedPlanet)
        	{
	            case "planet1Button":
					SceneManager.LoadScene("FruitGame");
                	break;
            	case "planet2Button":
                	SceneManager.LoadScene("DirectionGame");
                	break;
            	default:
                	Debug.Log("GG");
                	break;
        	}		
		}
    }
}
