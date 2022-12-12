using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using MailKit.Net.Smtp;

namespace Assets.Scripts_A_General
{
    public class startButton : MonoBehaviour
    {

        [SerializeField] private TMP_InputField userId;
        [SerializeField] private TextMeshProUGUI placeholderText;
        private Regex validUserIds;
        private string csvPath;
    
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
            validUserIds = new Regex(@"^[a-zA-Z0-9_]+$");
            csvPath = Application.dataPath;
            csvPath = csvPath + "/Resources/stats.csv";
            placeholderText.text = GameManager.instance.Language == "ENG"? "Enter user ID..." : "Inserisci nome utente...";
        }

        private void TaskOnClick()
        {
            if (validUserIds.IsMatch(userId.text))
            {
                BuildCsv();
                GameManager.instance.CurrentUserId = userId.text;
                Debug.Log("Selected user ID: " + GameManager.instance.CurrentUserId);
                
                GameManager.instance.UpdateGameState(GameManager.instance.CurrentUserId == "Doctor"
                    ? GameState.DoctorInterface
                    : GameState.Map);
            }
            else
            {
                userId.text = "";
                placeholderText.fontSize = 10.8f;
                placeholderText.color = Color.red;
                placeholderText.text = GameManager.instance.Language == "ENG"? "Only letters, numbers, and '_'" : "Solo lettere, numeri e '_'";
            }
        }

        private void BuildCsv()
        {
            if (!File.Exists(csvPath)){
                var csv = new StringBuilder();
                var first = "ID Game";
                var second = "Game Type";
                var third = "Game Phase";
                var fourth = "Player";
                var fifth = "Reaction Time Mean";
                var sixth = "Reaction Time Std";
                var seventh = "Errors Number";
                var eighth = "Kid Score";
                var ninth = "Date";
                var tenth = "SUS";
                var eleventh = "Kid Autoevaluation";
                var newLine = $"{first},{second},{third},{fourth},{fifth},{sixth},{seventh},{eighth},{ninth},{tenth},{eleventh}";
                csv.AppendLine(newLine);
                File.WriteAllText(csvPath, csv.ToString());
            }
        }
    }
}
