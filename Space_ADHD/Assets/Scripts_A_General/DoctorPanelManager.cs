using UnityEngine;

namespace Assets.Scripts_A_General
{
    public class DoctorPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject doctorPanel;

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
            doctorPanel.SetActive(state == GameState.DoctorInterface);
        }
    }
}