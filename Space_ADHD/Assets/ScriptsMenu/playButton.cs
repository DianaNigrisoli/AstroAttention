using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts_A_General;
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

	void Awake()
	{
		GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
	}

	void OnDestroy()
	{
		GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
	}

	private void GameManagerOnOnGameStateChanged(GameState state)
	{
		gameObject.GetComponent<Button>().interactable = state != GameState.Settings;
	}

	void Start()
    {
	    this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        this.moveKingBool = false;
        gameObject.SetActive(false);
		txtBox.enabled = false;
    }

    private void TaskOnClick()
    {
        selectedPlanet = buttonAction.selectedPlanet;
        Debug.Log("Starting Intro" + " " + selectedPlanet);
        this.transform.position = new Vector3(Screen.width*2, Screen.height*2, 50);
        GameObject.Find("ButtonSettings").SetActive(false);
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
                king1.GetComponent<AudioSource>().Play();
				kingHasSpoken = true;
                break;
            case "planet2Button":
                Debug.Log("Scene planet 2");
                GameObject king2 = GameObject.Find("kingPlanet2");
                Vector3 targetPosition2 = new Vector3(2.37f, 0.49f, 2.3f);
                moveKing(king2, targetPosition2);
				searchInCSV("Planet 2");
                king2.GetComponent<AudioSource>().Play();
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

        var path = GameManager.instance.Language == "ENG" ? "dialogs" : "dialogs_ita";
        TextAsset dialogs_csv = (TextAsset) Resources.Load(path);
        string dialogs = dialogs_csv.text;
        var lines = dialogs.Split('\n');

        foreach (string line in lines)
        {
	        if (!String.IsNullOrEmpty(line))
	        {
		        var values = line.Split(';');
		        listA.Add(values[0]);
		        listB.Add(values[1]);
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
		            GameManager.instance.UpdateGameState(GameState.FruitGame);
		            SceneManager.LoadScene("FruitGame");
                	break;
            	case "planet2Button":
	                GameManager.instance.UpdateGameState(GameState.DirectionGame);
	                SceneManager.LoadScene("DirectionGame");
                	break;
            	default:
                	Debug.Log("GG");
                	break;
        	}		
		}
    }
}
