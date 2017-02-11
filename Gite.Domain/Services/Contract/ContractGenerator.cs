using System;
using System.IO;
using Gite.Domain.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gite.Domain.Services.Contract
{
    public class ContractGenerator : IContractGenerator
    {
        private readonly string _baseUrl;
        private ParagraphBorder _borderManager;

        static Font _titleFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 13, Font.UNDERLINE);
        static Font _textFont = FontFactory.GetFont(FontFactory.TIMES, 12);
        static Font _underlinedFont = FontFactory.GetFont(FontFactory.TIMES, 12, Font.UNDERLINE);

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
                    _borderManager = new ParagraphBorder();
                    writer.PageEvent = _borderManager;
                    
                    document.Open();
                    AddDocumentTitle(document, reservation, string.Join("/", _baseUrl, "Content/Images/logo_small.png"));
                    AddDefinitions(document, reservation.Contact);
                    AddDesignation(document);
                    AddDuree(document, reservation.FirstWeek, reservation.LastWeek);
                    AddFamily(document, reservation.People);
                    AddLoyer(document, reservation);
                    AddCaution(document);
                    AddMenage(document);
                    AddEtatDesLieux(document);
                    AddTaxeSejour(document, reservation.People.Adults, (reservation.LastWeek.AddDays(7) - reservation.FirstWeek).Days);
                    AddConditionGenerales(document, reservation);
                    AddPriseEffets(document, reservation);
                    AddSignatures(document);

                    document.Close();

                    return new MemoryStream(stream.ToArray());
                }
            }
        }

        private void AddDefinitions(Document document, Contact contact)
        {
            _borderManager.IsBordered = true; // Add border...
            _borderManager.ParagraphCount = 2; // ... to the next 2 paragraphs.

            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("Entre les soussignés");
            document.Add(title);
            
            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };

            phrase.Add(new Phrase("M. BERLEMONT Roland et Mme. DUBY France demeurant à Rue du Longfaux 50, 7133 Buvrinnes (Belgique)\r\n"));
            phrase.Add(new Phrase("N° de téléphone belge : +32(0)486 34 99 99 - français : +33(0)6 56 74 23 10 ; ci-après désignés le propriétaire\r\n"));
            phrase.Add(new Phrase("\r\n"));
            phrase.Add(new Phrase(string.Format("{0} demeurant à {1}\r\n", contact.Name, contact.Address)));
            phrase.Add(new Phrase(string.Format("N° de téléphone : {0} ; ci-après désigné le locataire", contact.Phone)));

            document.Add(phrase);

            _borderManager.IsBordered = false;

            var separation = new Paragraph { Alignment = Element.ALIGN_LEFT, Font = _textFont };
            separation.Add(new Phrase("\r\n\r\n"));
            document.Add(separation);
        }

        private static void AddDesignation(Document document)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("1. Désignation:");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase("La location est prévue pour 6 personnes maximum et porte sur un meublé mitoyen situé: Veyrières 07380 Chirols."));
            phrase.Add(new Phrase("\r\n"));
            phrase.Add(new Phrase("Description de la location : gîte de 90m² comprenant séjour, coin cuisine et salon, salle d'eau, wc, 3 chambres, 2 terrasses et une buanderie.\r\nÉquipement complet. Draps non fournis.\r\n Forfait électricité 56kw/semaine, dépassement facturé au tarif EDF en vigueur. Forfait granulés pour le poêle : 4 sacs de 15kg de septembre à avril. Au-delà de ces 4 sacs ou de ces mois, tous sacs supplémentaires seront payants."));
            phrase.Add(new Phrase("Vous trouverez la description complète en deuxième pièce jointe et pour plus d'informations et de photos, vous pouvez consulter notre site (http://www.mas-des-genettes.fr)\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddDuree(Document document, DateTime begin, DateTime end)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("2. Durée :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase(string.Format("La location commencera le {0} à partir de 16h pour se terminer le {1} avant 10h\r\n", begin.ToString("dd/MM/yyyy"), end.AddDays(7).ToString("dd/MM/yyyy"))));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddFamily(Document document, People people)
        {
            var total = people.Adults + people.Children + people.Babies;

            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("3. Composition de famille :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase(string.Format("Ce contrat prévoit le séjour de {0} personnes dont {1} adultes, {2} enfants de 2 à 12 ans et {3} enfants de moins de 2 ans. {4} animaux {5} sont également prévus.", total, people.Adults, people.Children, people.Babies, people.Animals, string.IsNullOrEmpty(people.AnimalsDescription) ? "" : string.Format("({0})", people.AnimalsDescription))));
            phrase.Add(new Phrase("\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddLoyer(Document document, Reservation reservation)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("4. Loyer :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase(string.Format("Le montant de la location est de {0} euros avec certaines charges non comprises. Les charges non comprises dans le prix sont : forfait électricité (56kw/semaine), le dépassement sera facturé au tarif EDF en vigueur.  Les sacs de granulés de bois pour le poêle sont au prix de 4,50 euros le sac de 15kg. \r\n", reservation.FinalPrice.ToString("N"))));
            phrase.Add(new Phrase("Taxe de séjour applicable aux résidents âgés de plus de 12 ans.\r\n"));

            if (!reservation.IsLastMinute) // Only for normal reservations
                phrase.Add(new Phrase(string.Format("Un acompte sera versé par virement bancaire dès réception du contrat, d'un montant de {0} euros représentant 25% du prix de la location. ", (reservation.FinalPrice * 0.25).ToString("N"))));
            else
                phrase.Add(new Phrase("La totalité de la somme devra être versée par virement bancaire et reçue par le propriétaire au plus tard 48 heures avant l'arrivée du locataire. "));

            phrase.Add(new Phrase("En cas d'annulation par le locataire, le propriétaire pourra demander le paiement de l'intégralité du prix de la location.\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddCaution(Document document)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("5. Dépôt de garantie (caution) :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase("Un dépôt de garantie sera levé le jour de la prise de la possession des lieux d'un montant de 280,00 euros (chèque bancaire accepté). Il sera restitué le jour du départ ou au plus tard dans les 10 jours. Le dépôt de garantie ne doit pas être considéré par le locataire comme une participation au paiement du loyer. Il sert en cas de dégradations commises par le locataire. Si le montant des pertes excède le montant de ce dépôt, le locataire s'engage à régler le préjudice après l'inventaire de sortie. Le propriétaire s'engage à justifier du montant nécessaire à la remise en état du logement. En cas de non-règlement amiable, c'est le tribunal d'instance du lieu de situation de la location qui est compétent.\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddMenage(Document document)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("6. Ménage :");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase("Le locataire a la possibilité de prendre une option ménage au début de son séjour au tarif de 45,00 euros à payer lors de son départ. Si cette option n'est pas d'application, le logement doit être laissé propre et le ménage effectué avant le départ. Dans le cas contraire, les frais de ménage seront imputés sur la caution.\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddEtatDesLieux(Document document)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("7. État des lieux et inventaire:");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase("Un état des lieux et un inventaire seront établis contradictoirement à l'arrivée et à la sortie du locataire.\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddTaxeSejour(Document document, int nbPeople, int nbNights)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("8. Taxe de séjour:");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Chunk("Une taxe de séjour de 0,80 euros par jour par personne de 12 ans et plus est d'application.\r\n"));
            phrase.Add(new Chunk(string.Format("Dans ce cas: [0,80 x {0} (pers.)] x {1} (nuits) = ", nbPeople, nbNights)));
            phrase.Add(new Chunk(string.Format("{0} euros à payer le jour du départ.", (nbPeople*nbNights*0.80).ToString("N")), _underlinedFont));
            phrase.Add(new Chunk("\r\n"));
            phrase.Add(new Chunk("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private static void AddConditionGenerales(Document document, Reservation reservation)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("9. Conditions générales:");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phrase.Add(new Phrase("Le locataire s'engage à ne pas amener des personnes supplémentaires sans l'autorisation du propriétaire, à ne pas sous-louer le logement, à user paisiblement des lieux, à s'assurer contre les risques locatifs.\r\n"));

            if(!reservation.IsLastMinute)
                phrase.Add(new Phrase("Solde de la location à payer par virement bancaire 10 jours au plus tard avant le début de la location sous peine d'annulation de la location et l'acompte sera gardé. Paiement sur le compte:\r\n"));
            else
                phrase.Add(new Phrase("Somme totale de la location à payer par virement bancaire de manière à être reçue par le propriétaire au plus tard 48 heures avant l'arrivée du locataire sur le compte:\r\n"));

            phrase.Add(new Phrase("\r\n       M. BERLEMONT Roland\r\n       IBAN : FR2920041010052418245X02640\r\n       BIC : PSSTFRPPLIL\r\n\r\n"));
            phrase.Add(new Phrase("Ce contrat ne peut pas être rectifié sans mettre le nombre de mot et de ligne rayés et contresigné des deux parties.\r\n"));
            phrase.Add(new Phrase("\r\n"));

            document.Add(title);
            document.Add(phrase);
        }

        private void AddPriseEffets(Document document, Reservation reservation)
        {
            _borderManager.IsBordered = true; // Add border...
            _borderManager.ParagraphCount = 2;// ... to the next 2 paragraphs.

            var title = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _titleFont
            };
            title.Add("10. Prise d'effet du contrat:");

            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.TIMES, 12)
            };
            phrase.Add(new Phrase("La location prendra effet si nous recevons:\r\n"));

            if (!reservation.IsLastMinute) { 
                phrase.Add(new Phrase("  - Le contrat daté et signé ainsi que "));
                phrase.Add(new Chunk(string.Format("l'acompte de {0} euros avant le {1}", (reservation.FinalPrice * 0.25).ToString("N"), reservation.BookedOn.AddDays(7).ToString("dd/MM/yyyy")), _underlinedFont));
                phrase.Add(new Phrase("\r\n"));
                phrase.Add(new Phrase("  - Le "));
                phrase.Add(new Chunk(string.Format("solde de {0} euros réglé par virement bancaire avant le {1}", (reservation.FinalPrice * 0.75).ToString("N"), reservation.FirstWeek.AddDays(-10).ToString("dd/MM/yyyy")), _underlinedFont));
                phrase.Add(new Phrase("\r\n\r\n"));
            }
            else
            {
                phrase.Add(new Phrase("  - Le contrat daté et signé euros "));
                phrase.Add(new Chunk(string.Format("avant le {0}", reservation.FirstWeek.Date.AddDays(-2).ToString("dd/MM/yyyy")), _underlinedFont));
                phrase.Add(new Phrase("\r\n"));
                phrase.Add(new Phrase("  - La "));
                phrase.Add(new Chunk(string.Format("somme de {0} euros réglée par virement bancaire et reçue avant le {1}", (reservation.FinalPrice).ToString("N"), reservation.FirstWeek.Date.AddDays(-2).ToString("dd/MM/yyyy")), _underlinedFont));
                phrase.Add(new Phrase("\r\n\r\n"));
            }

            document.Add(title);
            document.Add(phrase);

            _borderManager.IsBordered = false;

            var separation = new Paragraph { Alignment = Element.ALIGN_LEFT, Font = _textFont };
            separation.Add(new Phrase("\r\n\r\n")); phrase.Add(new Phrase("\r\n"));
            separation.Add(new Phrase(string.Format("Fait à Buvrinnes, le {0}\r\n\r\n", reservation.BookedOn.ToString("dd/MM/yyyy"))));
            document.Add(separation);
        }

        private static void AddSignatures(Document document)
        {
            var phraseB = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = _textFont
            };
            phraseB.Add(new Phrase("Signature du locataire:                                                                              Signature du propriétaire:\r\n"));
            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 12)
            };
            phrase.Add(new Phrase("(précédée de la mention \"Lu et approuvé\")\r\n"));

            document.Add(phraseB);
            document.Add(phrase);
        }

        private static void AddDocumentTitle(Document document, Reservation reservation, string imageUrl)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_CENTER,
                Font = FontFactory.GetFont(FontFactory.HELVETICA, 20)
            };

            if(reservation.IsLastMinute)
                title.Add("Contrat de location de dernière minute\r\n\r\n\r\n\r\n");
            else
                title.Add("Contrat de location\r\n\r\n\r\n\r\n");

            var logo = Image.GetInstance(new Uri(imageUrl));
            logo.SetDpi(300, 300);
            logo.ScaleToFit(120f, 78f);
            logo.SetAbsolutePosition(document.PageSize.Width - 25f - 120f, document.PageSize.Height - 20f - 78f);

            document.Add(logo);
            document.Add(title);
        }
    }
}