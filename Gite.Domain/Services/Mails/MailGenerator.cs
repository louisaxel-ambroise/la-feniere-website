using Gite.Model.Model;
using System;

namespace Gite.Model.Services.Mails
{
    public class MailGenerator : IMailGenerator
    {
        private readonly string _baseUrl;

        public MailGenerator(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentNullException("baseUrl");

            _baseUrl = baseUrl;
        }

        public Mail GenerateMail(Reservation reservation)
        {
            return new Mail
            {
                Subject = "Mas des Genettes - Votre réservation",
                Content = string.Format(@"
<h2>Votre réservation.</h2>
<p>Votre réservation du {0} au {1}</p>
<p>Vous pouvez accéder aux détails à l'adresse suivante: <a href=""{2}"">{2}</a></p>
", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"),
string.Format("{0}/overview/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")))
            };
        }
    }
}
