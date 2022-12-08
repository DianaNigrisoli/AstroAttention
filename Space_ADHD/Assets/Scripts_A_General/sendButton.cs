using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net.Mail;
using System.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Assets.Scripts_A_General
{
    public class sendButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userId;
        [SerializeField] private TMP_InputField email;
        [SerializeField] private TextMeshProUGUI placeholderText;
        
        private string csvPath;
    
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
            csvPath = Application.dataPath;
            csvPath = csvPath + "/Resources/stats.csv";
        }

        private void TaskOnClick()
        {
            bool isValid = false;
            try
            {
                MailAddress address = new MailAddress(email.text);
                isValid = (address.Address == email.text);
            }
            catch (FormatException)
            {
                
            }
            if (isValid)
            {
                Debug.Log("Email sent to: " + email.text);
                Sendmail(email.text);
                GameManager.instance.UpdateGameState(GameState.UserSelection);
                userId.text = "";
            }
            else
            {
                email.text = "";
                placeholderText.fontSize = 10.8f;
                placeholderText.color = Color.red;
                placeholderText.text = "Incorrect address format";
            }
        }

        private void Sendmail(string receiverEmail)
        {      
            var message = new EmailMessage()
            {
                From = "group09manager@gmail.com",
                To = receiverEmail,
                MessageText = "Data received at " + DateTime.Now + " \n\nBest regards, Group09 :)",
                Subject = "Game scores data " + DateTime.Now
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
