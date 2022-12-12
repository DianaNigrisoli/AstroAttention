using System;
using Assets.Scripts_A_General;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScriptsMenu
{
    public class flagButton : MonoBehaviour
    {
    
        public static event Action OnLanguageChanged;
    
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            GameManager.instance.Language = GameManager.instance.Language == "ENG" ? "ITA" : "ENG";
            GameManager.instance.UpdateGameState(GameState.Settings);
            
            OnLanguageChanged?.Invoke();
        }
    }
}
