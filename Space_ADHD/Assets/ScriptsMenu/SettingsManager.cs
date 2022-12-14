using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using Assets.ScriptsMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshProUGUI textCurrentLanguage;
    [SerializeField] private Button flag;
    [SerializeField] private Texture textureItalian;
    [SerializeField] private Texture textureEnglish;
    [SerializeField] private Button logoutButton;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        flagButton.OnLanguageChanged += FlagButtonOnOnLanguageChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
        flagButton.OnLanguageChanged -= FlagButtonOnOnLanguageChanged;
    }

    void Start()
    {
        settingsPanel.SetActive(GameManager.instance.State == GameState.Settings || GameManager.instance.State == GameState.UserSelection);
        flag.gameObject.SetActive(GameManager.instance.State == GameState.Settings);
        logoutButton.gameObject.SetActive(GameManager.instance.State == GameState.Settings);
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        settingsPanel.SetActive(state is GameState.Settings or GameState.UserSelection or GameState.DoctorInterface);
        flag.gameObject.SetActive(state is GameState.Settings);
        logoutButton.gameObject.SetActive(state is GameState.Settings);
        if (state == GameState.Settings)
        {
            FlagButtonOnOnLanguageChanged();
        }
        else
        {
            textCurrentLanguage.text = "";
        }
    }
    
    private void FlagButtonOnOnLanguageChanged()
    {
        flag.GetComponent<RawImage>().texture = GameManager.instance.Language == "ITA" ? textureItalian : textureEnglish;
        textCurrentLanguage.text = GameManager.instance.Language == "ITA" ? "Lingua" : "Language";
    }
}
