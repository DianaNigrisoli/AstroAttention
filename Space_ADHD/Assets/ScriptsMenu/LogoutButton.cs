using System;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ScriptsMenu
{
    public class LogoutButton : MonoBehaviour
    {
    
        public static event Action OnLogout;
    
        void Awake()
        {
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
        }
    
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }
    
        private void TaskOnClick()
        {
            OnLogout?.Invoke();
            GameManager.instance.UpdateGameState(GameState.UserSelection);
            Destroy(GameObject.Find("GameManager"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void GameManagerOnOnGameStateChanged(GameState state)
        {
            gameObject.GetComponent<Button>().interactable = state == GameState.Settings;
        }
    
    }
}
