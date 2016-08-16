using System;
using Gite.Model.Aggregates;
using Gite.Model.Model;
using Gite.Model.Services.Contract;

namespace Gite.Model.Services.Mailing
{
    public class MailGenerator : IMailGenerator
    {
        private readonly IContractGenerator _contractGenerator;
        private readonly string _baseUrl;

        public MailGenerator(IContractGenerator contractGenerator, string baseUrl)
        {
            if (contractGenerator == null) throw new ArgumentNullException("contractGenerator");
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");

            _contractGenerator = contractGenerator;
            _baseUrl = baseUrl;
        }

        public Mail GenerateAdvancePaymentReceived(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = "Acompte déclaré payé",
                Content = new MailContent
                {
                    Attachments = new[]{ new MailAttachment
                    {
                        Data = _contractGenerator.GenerateForReservation(reservation),
                        Name = "contrat.pdf"
                    }},
                    IsHtml = true,
                    Content = string.Format(@"<p>L'acompte de la réservation du {0} au {1} a été déclaré payé</p>
<p>Pour valider la réception de l'acompte, suivre le lien: <a href=""{2}"">{2}</a></p>
", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"),
                    string.Format("{0}/reservations/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")))
                }
            };
        }

        public Mail GenerateReservationCreated(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = "Mas des Genettes - Votre réservation",
                Content = new MailContent
                {
                    Attachments = new []{ new MailAttachment
                    {
                        Data = _contractGenerator.GenerateForReservation(reservation),
                        Name = "contrat.pdf"
                    }},
                    IsHtml = true,
                    Content = string.Format(@"
<h2>Votre réservation.</h2>
<p>Votre réservation du {0} au {1}</p>
<p>Vous pouvez accéder aux détails à l'adresse suivante: <a href=""{2}"">{2}</a></p>
", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"),
                    string.Format("{0}/overview/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")))
                }
            };
        }
    }
}