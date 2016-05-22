using Gite.Model.Model;
using System;

namespace Gite.Model.Services.MailSender
{
    public class ReservationConfirmationMailSender : BaseMailSender, IReservationConfirmationMailSender
    {
        private readonly string _from;
        private readonly string _password;
        private readonly string _baseUrl;

        public ReservationConfirmationMailSender(string from, string password, string baseUrl)
        {
            _from = from;
            _password = password;
            _baseUrl = baseUrl;
        }

        public bool ConfirmReservation(Reservation reservation)
        {
            var mail = new Mail {
                Subject = "Gîte La Fenière - Réservation",
                Content = GetBody(reservation)
            };

            try
            {
                SendMail(_from, _password, reservation.Contact.Mail, mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetBody(Reservation reservation)
        {
            var overviewLink = _baseUrl + "/overview/details/" + reservation.Id.ToString("D");

            return string.Format("Votre réservation du {0} au {1} a été validée.\r\n Veuillez suivre le lien suivant afin de procéder au paiement: {2}",
                reservation.StartingOn.ToString("yyyy-MM-dd"),
                reservation.EndingOn.ToString("yyyy-MM-dd"),
                overviewLink);
        }
    }
}
