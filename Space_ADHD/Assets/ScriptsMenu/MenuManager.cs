using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI suggestion;
    private float suggestionDelay = 7.5f;
    private Boolean startWriting;
    public static Boolean planetSelected;
    private String suggestionText = "Let's explore some planets";
    private String displayedSuggestionText = "";
    private float writerTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        planetSelected = false;
        startWriting = false;
    }

    // Update is called once per frame
    void Update()
    {
        suggestionDelay -= Time.deltaTime;
        if (planetSelected)
        {
            suggestion.SetText("");
        }else if (startWriting)
        {
            writerTimer += Time.deltaTime;
            if (writerTimer / 0.9f > 0)
            {
                if(displayedSuggestionText.Length < suggestionText.Length)
                    displayedSuggestionText += suggestionText[displayedSuggestionText.Length];
                suggestion.SetText(displayedSuggestionText);
                writerTimer = 0.0f;
            }
            
            
        }
        else if(suggestionDelay <= 0)
        {
            startWriting = true;
        }
    }
}
