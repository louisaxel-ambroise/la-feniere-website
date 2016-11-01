using System;
using System.IO;
using Gite.Model.Aggregates;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gite.Model.Services.Contract
{
    public class FicheDescriptiveGenerator : IFicheDescriptiveGenerator
    {
        private readonly string _baseUrl;
        private ParagraphBorder _borderManager;

        static Font _titleFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 13, Font.UNDERLINE);
        static Font _textFont = FontFactory.GetFont(FontFactory.TIMES, 12);

        public FicheDescriptiveGenerator(string baseUrl)
        {
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");

            _baseUrl = baseUrl;
        }

        public Stream GenerateForReservation(ReservationAggregate reservation)
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
                    AddDescription(document);

                    document.Close();

                    return new MemoryStream(stream.ToArray());
                }
            }
        }

        private void AddDescription(Document document)
        {
            var phrase = new Paragraph
            {
                Alignment = Element.ALIGN_LEFT,
                Font = FontFactory.GetFont(FontFactory.TIMES, 12)
            };

            phrase.Add(new Phrase("  - Gîte mitoyen de plus de 90 m2 (6 pers.) sur 2 niveaux avec jardin, 2 terrasses et une buanderie.\r\n\r\n"));
            phrase.Add(new Phrase("  - 1er niveau : séjour, salon, poêle à granulés, cuisine équipée, salle d’eau (avec douche), WC, 1 chambre d’un lit de 2 personnes (140 x 200) et un lit bébé.\r\n\r\n"));
            phrase.Add(new Phrase("  - 2ème niveau : 1 chambre avec un lit de 2 personnes (140 x 200), 1 chambre avec 2 lits d’une personne (90 x 200).\r\n\r\n"));
            phrase.Add(new Phrase("  - Équipements : lave-linge, lave-vaisselle, tv, micro-onde, frigo, congélateur, poêle à granulés, fer et planche à repasser , équipement pour bébé(chaise, lit, baignoire et coussin à langer),meubles de jardin sur 2 terrasses, barbecue, relaxs.\r\n\r\n"));
            phrase.Add(new Phrase("  - Parking\r\n\r\n"));
            phrase.Add(new Phrase("  - Animaux acceptés\r\n\r\n"));
            phrase.Add(new Phrase("  - Option ménage\r\n\r\n"));

            document.Add(phrase);
        }

        private void AddDocumentTitle(Document document, ReservationAggregate reservation, string imageUrl)
        {
            var title = new Paragraph
            {
                Alignment = Element.ALIGN_CENTER,
                Font = FontFactory.GetFont(FontFactory.HELVETICA, 20)
            };

            title.Add("Fiche descriptive du gîte\r\n\r\n\r\n\r\n");

            var logo = Image.GetInstance(new Uri(imageUrl));
            logo.SetDpi(300, 300);
            logo.ScaleToFit(120f, 78f);
            logo.SetAbsolutePosition(document.PageSize.Width - 25f - 120f, document.PageSize.Height - 20f - 78f);

            document.Add(logo);
            document.Add(title);
        }
    }
}
