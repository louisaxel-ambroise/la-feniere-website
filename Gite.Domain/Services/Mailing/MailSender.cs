using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Gite.Domain.Model;

namespace Gite.Domain.Services.Mailing
{
    public class MailSender : IMailSender
    {
        private readonly string _password;
        private readonly string _from;
        private readonly IMailGenerator _mailGenerator;

        public MailSender(string from, string password, IMailGenerator mailGenerator)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (password == null) throw new ArgumentNullException("password");
            if (mailGenerator == null) throw new ArgumentNullException("mailGenerator");

            _from = from;
            _password = password;
            _mailGenerator = mailGenerator;
        }

        public void SendReservationCreated(Reservation reservation)
        {
            var clientMail = _mailGenerator.GenerateReservationCreated(reservation);
            var adminMail = _mailGenerator.GenerateReservationCreated(reservation);

            SendMail(adminMail, _from);
            SendMail(clientMail, reservation.Contact.Mail);
        }

        public void SendAdvancePaymentDeclared(Reservation reservation)
        {
            var mail = _mailGenerator.GenerateAdvancePaymentDeclared(reservation);

            SendMail(mail, _from);
        }

        public void SendPaymentDeclared(Reservation reservation)
        {
            var mail = _mailGenerator.GeneratePaymentDeclared(reservation);

            SendMail(mail, _from);
        }

        public void SendReservationCancelled(Reservation reservation)
        {
            var mail = _mailGenerator.GenerateReservationCancelled(reservation);

            SendMail(mail, reservation.Contact.Mail, _from);
        }

        public void SendAdvancePaymentReceived(Reservation reservation)
        {
            var mail = _mailGenerator.GenerateAdvancePaymentReceived(reservation);

            SendMail(mail, reservation.Contact.Mail);
        }

        public void SendFinalPaymentReceived(Reservation reservation)
        {
            var mail = _mailGenerator.GenerateFinalPaymentReceived(reservation);

            SendMail(mail, reservation.Contact.Mail);
        }

        private void SendMail(Mail message, string address, string bcc = null)
        {
            var credentials = new NetworkCredential(_from, _password);

            using (var mailMessage = new MailMessage(_from, address) { Subject = message.Subject, Body = message.Content.Content, IsBodyHtml = message.Content.IsHtml })
            using (var smtp = new SmtpClient { Host = "smtp.gmail.com", DeliveryMethod = SmtpDeliveryMethod.Network, EnableSsl = true, UseDefaultCredentials = true, Credentials = credentials, Port = 587 })
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