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
                    IsHtml = true,
                    Content = string.Format(@"<p>L'acompte de la réservation du {0} au {1} a été déclaré payé</p><p>Pour valider la réception de l'acompte, suivre le lien: <a href=""{2}"">{2}</a></p>", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"), string.Format("{0}/reservations/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")))
                }
            };
        }

        public Mail GenerateReservationCreated(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = "Votre réservation au Mas des Genettes",
                Content = new MailContent
                {
                    Attachments = new []{ new MailAttachment { Data = _contractGenerator.GenerateForReservation(reservation), Name = "contrat.pdf" }},
                    IsHtml = true,
                    Content = string.Format(@"<p>Madame, Monsieur,</p><p>Nous vous remercions pour l'intérêt que vous portez à notre gîte. Ci-joint le contrat de location pré-rempli avec les informations que vous nous avez transmises.<br />Si vous confirmez votre location, veuillez nous envoyer le contrat signé dans les 5 jours (de préférence par mail à cette adresse, ou par courrier à l'adresse postale suivante: Rue du Longfaux, 50. 7133 Buvrinnes, Belgique (le courrier devant nous parvenir au plus tard le {0}), et nous envoyer l'acompte de {1} euros sur le compte bancaire indiqué dans le contrat, avec en message les dates de votre réservation ({2}-{3}) afin que nous puissions bloquer votre réservation.<br />Nous seront ravis de vous accueillir dans notre chaleureux gîte. Au plaisir de recevoir de vos nouvelles.</p>
<p>Très cordialement,</p><p>France et Roland Berlemont, propriétaires du gîte ""Au Mas des Genettes""</p>", reservation.BookedOn.AddDays(5).ToString("dd/MM/yyyy"), (reservation.FinalPrice*0.25).ToString("N"), reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy")) // , string.Format("{0}/overview/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")) // URL of the site
                }
            };
        }

        public Mail GenerateNewReservationAdmin(ReservationAggregate reservation)
        {
            var people = string.Format("{0} adultes, {1} enfants, {2} bébés et {3} animaux {4}", reservation.People.Adults, reservation.People.Children, reservation.People.Babies, reservation.People.Animals, reservation.People.Animals > 0 ? string.Format("({0})", reservation.People.AnimalsDescription) : string.Empty);
            return new Mail
            {
                Subject = "Mas des Genettes - Nouvelle réservation",
                Content = new MailContent
                {
                    IsHtml = true,
                    Content = string.Format(@"<h2>Nouvelle réservation : du {0} au {1}</h2><p>Contact:</p><ul><li>Nom: {2}</li><li>Tel.: {3}</li><li>Adresse: {4}</li><li>Détails: {5}</li></ul>", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"), reservation.Contact.Name, reservation.Contact.Phone, reservation.Contact.Address, people)
                }
            };
        }

        public Mail GenerateFinalPaymentReceived(ReservationAggregate reservation)
        {
            throw new NotImplementedException();
        }
    }
}