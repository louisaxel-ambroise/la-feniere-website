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

        public Mail GenerateReservationCreated(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = "Votre réservation au Mas des Genettes",
                Content = new MailContent
                {
                    Attachments = new []{ new MailAttachment { Data = _contractGenerator.GenerateForReservation(reservation), Name = "contrat.pdf" }},
                    IsHtml = true,
                    Content = string.Format(@"<p>Madame, Monsieur,</p><p>Nous vous remercions pour l'intérêt que vous portez à notre gîte. Ci-joint le contrat de location pré-rempli avec les informations que vous nous avez transmises.<br />Si vous confirmez votre location, veuillez nous envoyer le contrat signé dans les 5 jours (de préférence par mail à cette adresse, ou par courrier à l'adresse postale suivante: Rue du Longfaux, 50. 7133 Buvrinnes, Belgique (le courrier devant nous parvenir au plus tard le {0}), et nous envoyer l'acompte de {1} euros sur le compte bancaire indiqué dans le contrat, avec en message les dates de votre réservation ({2}-{3}) afin que nous puissions confirmer votre réservation.</p>
<p>Vous pouvez à tout moment gérer votre réservation sur notre site à l'adresse suivante : <a href=""{4}"">{4}</a></p>
<p>Nous serions ravis de vous accueillir dans notre chaleureux gîte et restons à votre entière disposition pour toute information supplémentaire par téléphone (+32(0)486/34.99.99) ou par mail. Au plaisir de recevoir de vos nouvelles.</p>
<p>Très cordialement,</p><p>France et Roland Berlemont, propriétaires du gîte ""Au Mas des Genettes""</p>", reservation.BookedOn.AddDays(5).ToString("dd/MM/yyyy"), (reservation.FinalPrice*0.25).ToString("N"), reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"), string.Format("{0}/overview/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D"))) // , string.Format("{0}/overview/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")) // URL of the site
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

        public Mail GenerateAdvancePaymentDeclared(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = string.Format("Mas des Genettes - Déclaration de paiement d'acompte"),
                Content = new MailContent
                {
                    IsHtml = true,
                    Content = string.Format(@"<h2>Acompte déclaré payé : du {0} au {1}</h2><p>Contact:</p><ul><li>Nom: {2}</li><li>Tel.: {3}</li><li>Adresse: {4}</li><li>Lien pour valider: {5}</li></ul>", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"), reservation.Contact.Name, reservation.Contact.Phone, reservation.Contact.Address, string.Format("{0}/reservations/details/{0}", _baseUrl, reservation.Id))
                }
            };
        }

        public Mail GenerateAdvancePaymentReceived(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = string.Format("Reception de l'acompte de la réservation du {0} au {1} au Mas des Genettes", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy")),
                Content = new MailContent
                {
                    IsHtml = true,
                    Content = string.Format(@"<p>Madame, Monsieur,</p><p>Nous vous confirmons la réception du paiement de l'acompte pour votre réservation du {0} au {1}.</p><p>Très cordialement,</p><p>France et Roland Berlemont, propriétaires du gîte ""Au Mas des Genettes""", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"), string.Format("{0}/reservations/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")))
                }
            };
        }

        public Mail GenerateFinalPaymentReceived(ReservationAggregate reservation)
        {
            return new Mail
            {
                Subject = string.Format("Reception du paiement de la location du {0} au {1} au Mas des Genettes", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy")),
                Content = new MailContent
                {
                    IsHtml = true,
                    Content = string.Format(@"<p>Madame, Monsieur,</p><p>Nous vous confirmons la réception du paiement de la location pour votre réservation du {0} au {1}.<br />Nous vous souhaitons d'ors et déjà un très bon séjour.</p><p>Très cordialement,</p><p>France et Roland Berlemont, propriétaires du gîte ""Au Mas des Genettes""", reservation.FirstWeek.ToString("dd/MM/yyyy"), reservation.LastWeek.AddDays(7).ToString("dd/MM/yyyy"), string.Format("{0}/reservations/details/{1}", _baseUrl.TrimEnd('/'), reservation.Id.ToString("D")))
                }
            };
        }
    }
}