using System.Collections;
using System.Collections.Generic;
using Assets.Scripts_A_General;
using UnityEngine;

public class UserSelectionManager : MonoBehaviour
{

    [SerializeField] private GameObject userSelectionPanel;

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
        userSelectionPanel.SetActive(state == GameState.UserSelection);
    }
}
