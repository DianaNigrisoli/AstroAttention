using System;
using System.Collections;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ScriptsMenu
{
    public class LogoutButton : MonoBehaviour
    {
    
        public static event Action OnLogout;
        [SerializeField] private GameObject loadingScreen;
        
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
            GameObject temp = Instantiate(loadingScreen);
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(waiter());

        }

        private void GameManagerOnOnGameStateChanged(GameState state)
        {
            gameObject.GetComponent<Button>().interactable = state == GameState.Settings;
        }
        
        IEnumerator waiter()
        {
            yield return new WaitForSeconds(2f);
            OnLogout?.Invoke();
            GameManager.instance.UpdateGameState(GameState.UserSelection);
            Destroy(GameObject.Find("GameManager"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    
    }
}
