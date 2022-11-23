using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI suggestion;
    private Vector3 suggestionPosition;
    private float suggestionDelay = 6.5f;
    private Boolean startWriting;
    public static Boolean planetSelected;
    private String suggestionText = "Let's explore some planets!";
    private String displayedSuggestionText = "";
    private float writerTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        planetSelected = false;
        startWriting = false;
        suggestionPosition = suggestion.transform.position;
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
