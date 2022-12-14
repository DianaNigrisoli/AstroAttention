using MimeKit;
using UnityEngine;

namespace Assets.Scripts_A_General
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }

        public MimeMessage GetMessage()
        {
            var body = MessageText;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Group09Manager", From));
            message.To.Add(new MailboxAddress("", To));
            message.Subject = Subject;
            // message.Body = new TextPart("plain") { Text = body };

            var multipartBody = new Multipart("mixed");
            {
                var textPart = new TextPart( "plain" )
                {
                    Text = body
                };
                multipartBody.Add(textPart);

                string attachmentPath = PlayerPrefs.GetString("csvPrefs");
                var attachmentPart = new TextPart( "file/csv" )
                {
                    Text = attachmentPath
                };
                multipartBody.Add( attachmentPart );
            }
            message.Body = multipartBody;
            
            return message;
        }
    }
}