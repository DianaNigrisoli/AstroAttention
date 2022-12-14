using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.ScriptsDirectionGame;
using System.IO;
using Assets.Scripts_A_General;
using UnityEngine.SceneManagement;

public class submitButton : MonoBehaviour
{
	private string csvPath;
	private int currentID;
	private string firstValueLastLine;
	private int result;
	public static int susValue = -1;
	public static int evalValue = -1;
	[SerializeField] private GameObject loadingScreen;
	// Start is called before the first frame update
    void Start()
    {
	    csvPath = PlayerPrefs.GetString("csvPrefs");
		int csvLen = csvPath.Split("\n").Length;
		if ((csvLen % 4) == 2)
		{
			currentID = (csvLen-2)/4;
		}
		else
		{
			currentID = -1;
		}
		Debug.Log("Game ID: " + currentID);
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
		if (susValue != -1 && evalValue != -1)
		{
		    DateTime submitDate = DateTime.Now;
			for (int i = 0; i < 4; i++)
			{
				var IDgame = currentID;
	            var gameType = "Direction";
	            var gamePhase = i;
	            var player = GameManager.instance.CurrentUserId;
	            var reactionTimeMean = CannonBehavior.reactionTimeMean[i];
	            var reactionTimeStd = CannonBehavior.reactionTimeStd[i];
	            var errorsNumber = CannonBehavior.errorsDirectionG[i];
				var kidScore = "-";
				if (i==3)
				{
	                kidScore = string.Format("{0:N2}", CannonBehavior.kidScoreDirectionG);
				}
	            var date = submitDate;
	            var sus = susValue;
	            var kidAutoevaluation = evalValue;
	            var newLine = $"{IDgame},{gameType},{gamePhase},{player},{reactionTimeMean},{reactionTimeStd},{errorsNumber},{kidScore},{date},{sus},{kidAutoevaluation}\n";
	            PlayerPrefs.SetString("csvPrefs", PlayerPrefs.GetString("csvPrefs") + newLine);
			}
			GameObject temp = Instantiate(loadingScreen);
			loadingScreen.gameObject.SetActive(true);
	        GameManager.instance.UpdateGameState(GameState.Map);
	        SceneManager.LoadScene("Menu");
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
