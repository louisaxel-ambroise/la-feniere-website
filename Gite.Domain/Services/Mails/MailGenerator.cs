using Gite.Model.Model;
using Gite.Model.Services.Contract;
using System;

namespace Gite.Model.Services.Mails
{
    public class MailGenerator : IMailGenerator
    {
        private readonly string _baseUrl;
        private readonly IContractGenerator _contractGenerator;

        public MailGenerator(string baseUrl, IContractGenerator contractGenerator)
        {
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentNullException("baseUrl");
            if (contractGenerator == null) throw new ArgumentNullException("contractGenerator");

            _baseUrl = baseUrl;
            _contractGenerator = contractGenerator;
        }

        public Mail GenerateMail(Reservation reservation)
        {
            return new Mail
            {
                Subject = "Mas des Genettes - Votre réservation",
                Attachments = new[] { _contractGenerator.GenerateForReservation(reservation) },
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
