using Gite.Model.Model;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace Gite.Model.Services.Mails
{
    public class MailSender : IMailSender
    {
        private readonly string _from;
        private readonly string _password;

        public MailSender(string from, string password)
        {
            _from = from;
            _password = password;
        }

        public void SendMail(Mail message, string address)
        {
            var credentials = new NetworkCredential(_from, _password);

            using (var mailMessage = new MailMessage(_from, address) { Subject = message.Subject, Body = message.Content, IsBodyHtml = true })
            using (var smtp = new SmtpClient { Host = "smtp.gmail.com", EnableSsl = true, UseDefaultCredentials = true, Credentials = credentials, Port = 25 })
            {
                AddAttachments(mailMessage, message.Attachments);

                smtp.Send(mailMessage);
            }
        }

        private static void AddAttachments(MailMessage mailMessage, Stream[] attachments)
        {
            foreach(var stream in attachments)
            {
                ContentType contentType = new ContentType(MediaTypeNames.Application.Pdf);
                Attachment attach = new Attachment(stream, contentType);
                attach.ContentDisposition.FileName = "contract.pdf";

                mailMessage.Attachments.Add(attach);
            }
        }
    }
}
