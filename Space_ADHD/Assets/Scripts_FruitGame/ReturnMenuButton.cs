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

public class ReturnMenuButton : MonoBehaviour
{
    public static Boolean ReturnMenu = false;
    private GameObject[] allObject;
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
        // instructionsManager.showingTutorial = false;
        // IntroManager.showingTutorial = false;
        
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

