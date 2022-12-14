using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.ScriptsMenu;
using TMPro;
using UnityEngine;

public class MenuSuggestionController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI suggestion;
    private Vector3 suggestionPosition;
    private float suggestionDelay = 6.5f;
    private Boolean startWriting;
    public static Boolean planetSelected;
    private String suggestionText;
    private String displayedSuggestionText = "";
    private float writerTimer = 0.0f;

    private GameState gameState;
    
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        flagButton.OnLanguageChanged += OnSettingEvent;
        LogoutButton.OnLogout += OnSettingEvent;
    }

    private void OnSettingEvent()
    {
        suggestion.SetText("");
        displayedSuggestionText = "";
        writerTimer = 0.0f;
        suggestionText = GameManager.instance.Language == "ITA"
            ? "Oh no! La navicella è rotta. Visitiamo questi pianeti per chiedere aiuto"
            : "Oh no! My spaceship is broken! Let's stop by these planets and ask for help";
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
        flagButton.OnLanguageChanged -= OnSettingEvent;
        LogoutButton.OnLogout -= OnSettingEvent;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        gameState = state;
        if (state == GameState.Map)
        {
            suggestionDelay = 0.7f;
        }else if (state == GameState.Settings)
        {
            suggestion.SetText("");
            displayedSuggestionText = "";
            writerTimer = 0.0f;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        planetSelected = false;
        startWriting = false;
        suggestionPosition = suggestion.transform.position;
        suggestionText = PlayerPrefs.GetString("language") == "ITA"
            ? "Oh no! La navicella è rotta. Visitiamo questi pianeti per chiedere aiuto"
            : "Oh no! My spaceship is broken! Let's stop by these planets and ask for help";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameState.Map) return;
        suggestionDelay -= Time.deltaTime;
        if (planetSelected)
        {
            suggestion.SetText("");
        }else if (startWriting)
        {
            writerTimer += Time.deltaTime;
            if (writerTimer > 0.05*displayedSuggestionText.Length)
            {
                if(displayedSuggestionText.Length < suggestionText.Length)
                    displayedSuggestionText += suggestionText[displayedSuggestionText.Length];
                suggestion.SetText(displayedSuggestionText);
            }
            
            
        }
        else if(suggestionDelay <= 0)
        {
            startWriting = true;
        }

        suggestion.transform.position = suggestionPosition +
            new Vector3(Mathf.Sin(Time.time * 2.3f), -Mathf.Sin(Time.time * 2.3f), 0);
    }
}
