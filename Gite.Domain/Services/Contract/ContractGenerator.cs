using Gite.Model.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace Gite.Model.Services.Contract
{
    public class ContractGenerator : IContractGenerator
    {
        private readonly string _baseUrl;

        public ContractGenerator(string baseUrl)
        {
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");

            _baseUrl = baseUrl;
        }

        public Stream GenerateForReservation(Reservation reservation)
        {
            using (var stream = new MemoryStream())
            {
                var document = new Document();

                using (var writer = PdfWriter.GetInstance(document, stream))
                {
                    document.Open();
                    AddDocumentTitle(document, string.Join("/", _baseUrl, "Content/Images/logo_small.png"));
                    AddDefinitions(document, reservation.Contact);
                    AddDuree(document, reservation.FirstWeek, reservation.LastWeek);
                    AddLoyer(document, reservation);
                    document.Close();

                    return new MemoryStream(stream.ToArray());
                }
            }
        }

        private static void AddDefinitions(Document document, Contact contact)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, Font.UNDERLINE)
            };
            title.Add("Entre les soussignés");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.HELVETICA, 12)
            };
            phrase.Add(new Phrase("M. BERLEMONT Roland et Mme. DUBY France demeurant à Rue du Longfaux 50, 7133 Buvrinnes (Belgique)\r\n"));
            phrase.Add(new Phrase("N° de téléphone : 0032486/34.99.99 ; ci-après désignés le propriétaire\r\n"));
            phrase.Add(new Phrase("\r\n"));
            phrase.Add(new Phrase(string.Format("{0} demeurant à {1}\r\n", contact.Name, contact.Address)));
            phrase.Add(new Phrase(string.Format("N° de téléphone : {0} ; ci-après désigné le locataire\r\n", contact.Phone)));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddDesignation(Document documet)
        {

        }

        private static void AddDuree(Document document, DateTime begin, DateTime end)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, Font.UNDERLINE)
            };
            title.Add("2. Durée :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.HELVETICA, 12)
            };
            phrase.Add(new Phrase(string.Format("La location commencera le {0} à partir de 16h pour se terminer le {1} avant 11h\r\n", begin.ToString("dd/MM/yyyy"), end.AddDays(7).ToString("dd/MM/yyyy"))));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddLoyer(Document document, Reservation reservation)
        {
            var duration = (reservation.LastWeek.AddDays(7) - reservation.FirstWeek).Days;
            var taxeSejour = duration * 0.80 * reservation.People.Adults; // Taxe de séjour
            var overpopulation = reservation.People.Adults + reservation.People.Children + reservation.People.Babies - 6;

            var totalPrice = reservation.FinalPrice + taxeSejour + overpopulation*30;

            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, Font.UNDERLINE)
            };
            title.Add("3. Loyer :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.HELVETICA, 12)
            };
            phrase.Add(new Phrase(string.Format("Le montant de la location est de {0}€ (euros) toutes charges comprises\r\n", reservation.FinalPrice)));
            phrase.Add(new Phrase(string.Format("Un accompte sera versé dès réception du contrat, d'un montant de {0}€ (euros) représentant 25% du prix de la location. En cas d'annulation par le locataire, le bailleur pourra demander le paiement de l'intégralité du prix de la location.\r\n", reservation.FinalPrice * 0.25)));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddDocumentTitle(Document document, string imageUrl)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_CENTER,
                Font = FontFactory.GetFont(FontFactory.HELVETICA, 28)
            };
            title.Add("Contrat de location\r\n\r\n\r\n");

            var logo = Image.GetInstance(new Uri(imageUrl));
            logo.SetAbsolutePosition(document.PageSize.Width - 36f - 180f, document.PageSize.Width - 36f - 180f);

            document.Add(logo);
            document.Add(title);
        }
    }
}
