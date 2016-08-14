using Gite.Model.Model;
using System.Net;
using System.Net.Mail;

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
                smtp.Send(mailMessage);
            }
        }
    }
}
