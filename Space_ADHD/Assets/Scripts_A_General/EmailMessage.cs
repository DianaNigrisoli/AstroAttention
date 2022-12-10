using System.IO;
using MimeKit;
using Application = UnityEngine.Application;

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
                
                string attachmentPath = Application.dataPath + "/Resources/stats.csv";
                var attachmentPart = new MimePart( "file/csv" )
                {
                    Content = new MimeContent( File.OpenRead( attachmentPath ), ContentEncoding.Default ),
                    ContentDisposition = new ContentDisposition( ContentDisposition.Attachment ),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName( attachmentPath )
                };
                multipartBody.Add( attachmentPart );
            }
            message.Body = multipartBody;
            
            return message;
        }
    }
}