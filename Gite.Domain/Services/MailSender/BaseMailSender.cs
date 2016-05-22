using System.Net;
using System.Net.Mail;

namespace Gite.Model.Services.MailSender
{
    public abstract class BaseMailSender
    {
        protected void SendMail(string from, string password, string to, Mail message)
        {
            var credentials = new NetworkCredential(from, password);

            using (var mailMessage = new MailMessage(from, to) { Subject = message.Subject, Body = message.Content, IsBodyHtml = false })
            using (var smtp = new SmtpClient { Host = "smtp.gmail.com", EnableSsl = true, UseDefaultCredentials = true, Credentials = credentials, Port = 587})
            {
                smtp.Send(mailMessage);
            }
        }
    }
}
