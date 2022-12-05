using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts_A_General
{
    public class startButton : MonoBehaviour
    {

        [SerializeField] private TMP_InputField userId;
        [SerializeField] private TextMeshProUGUI placeholderText;
        private Regex validUserIds;
    
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
            validUserIds = new Regex(@"^[a-zA-Z0-9_]+$");
        }

        private void TaskOnClick()
        {
            if (validUserIds.IsMatch(userId.text))
            {
                GameManager.instance.CurrentUserId = userId.text;
                Debug.Log("Selected user ID: " + GameManager.instance.CurrentUserId);
                GameManager.instance.UpdateGameState(GameState.Map);
            }
            else
            {
                userId.text = "";
                placeholderText.fontSize = 10.8f;
                placeholderText.color = Color.red;
                placeholderText.text = "Only letters, numbers, and '_'";
            }
        }
    }
}
