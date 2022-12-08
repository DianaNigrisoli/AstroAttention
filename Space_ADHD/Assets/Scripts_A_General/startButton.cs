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
        }

        private void TaskOnClick()
        {
            if (validUserIds.IsMatch(userId.text))
            {
                GameManager.instance.CurrentUserId = userId.text;
                Debug.Log("Selected user ID: " + GameManager.instance.CurrentUserId);
                GameManager.instance.UpdateGameState(GameState.Map);
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
            else
            {
                userId.text = "";
                placeholderText.fontSize = 10.8f;
                placeholderText.color = Color.red;
                placeholderText.text = "Only letters, numbers, and '_'";
            }

            Sendmail();
        }

        private void Sendmail()
        {      
            var message = new EmailMessage()
            {
                From = "group09manager@gmail.com",
                To = "raffo24999@gmail.com",
                MessageText = "test",
                Subject = "test at " + DateTime.Now
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(message.From, "qkaxevtbriorwtut");
                    client.Send(message.GetMessage());
                    client.Disconnect(true);
                }
            
            
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}
