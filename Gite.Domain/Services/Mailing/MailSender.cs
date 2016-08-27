using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public class MailSender : IMailSender
    {
        private readonly string _password;
        public string From { get; private set; }

        public MailSender(string from, string password)
        {
            if (@from == null) throw new ArgumentNullException("from");
            if (password == null) throw new ArgumentNullException("password");

            From = @from;
            _password = password;
        }

        public void SendMail(Mail message, string address, string bcc = null)
        {
            var credentials = new NetworkCredential(From, _password);

            using (var mailMessage = new MailMessage(From, address) { Subject = message.Subject, Body = message.Content.Content, IsBodyHtml = message.Content.IsHtml })
            using (var smtp = new SmtpClient { Host = "smtp.gmail.com", EnableSsl = true, UseDefaultCredentials = true, Credentials = credentials, Port = 25 })
            {
                if (!string.IsNullOrEmpty(bcc)) mailMessage.Bcc.Add(new MailAddress(bcc));
                AddAttachments(mailMessage, message.Content.Attachments.ToArray());

                smtp.Send(mailMessage);
            }
        }

        private static void AddAttachments(MailMessage mailMessage, MailAttachment[] attachments)
        {
            foreach (var attachment in attachments)
            {
                var contentType = new ContentType(MediaTypeNames.Application.Pdf);
                var attach = new Attachment(attachment.Data, contentType);
                attach.ContentDisposition.FileName = attachment.Name;

                mailMessage.Attachments.Add(attach);
            }
        }
    }
}