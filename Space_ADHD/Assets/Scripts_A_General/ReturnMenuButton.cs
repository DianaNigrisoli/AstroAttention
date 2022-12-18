using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Assets.Scripts_A_General;
using Assets.Scripts_FruitGame;
using UnityEngine.SceneManagement;
using System.Globalization;
using SkipTutorialButton = Assets.Scripts_FruitGame.SkipTutorialButton;

public class ReturnMenuButton : MonoBehaviour
{
    public static Boolean ReturnMenu = false;
    private GameObject[] allObject;
    [SerializeField] private GameObject loadingScreen;
    
    void Awake()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged += MiniGameManagerOnOnMiniGameStateChanged;
    }

    void OnDestroy()
    {
        MiniGameManagerFruit.OnMiniGameStateChanged -= MiniGameManagerOnOnMiniGameStateChanged;
    }

    private void MiniGameManagerOnOnMiniGameStateChanged(MiniGameStateFruit state)
    {
        gameObject.SetActive(true);
        allObject = FindObjectsOfType<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        GameObject temp = Instantiate(loadingScreen);
        loadingScreen.gameObject.SetActive(true);
        
        //Fruit Game restoring variables
        SkipTutorialButton.TutorialSkipped = false;
        PlayerMovement.score = 0;
        PlayerMovement.ListScore.Clear();
        PlayerMovement.ListReactionTime.Clear();
        //********
        
        ReturnMenu = true;
        GameManager.instance.UpdateGameState(GameState.Map);
        SceneManager.LoadScene("Menu");

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

