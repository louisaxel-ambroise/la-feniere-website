using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gite.Model.Services.Contract
{
    public class ParagraphBorder : PdfPageEventHelper {
        public float Offset = 5;
        public float StartPosition;

        public bool IsBordered { get; set; }

        public override void OnParagraph(PdfWriter writer, Document document, float paragraphPosition) {
            StartPosition = paragraphPosition;
        }

        public override void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) {
            if (!IsBordered) return;

            var cb = writer.DirectContentUnder;
            cb.Rectangle(document.Left, paragraphPosition - Offset, document.Right - document.Left, StartPosition - paragraphPosition);
            cb.Stroke();
        }
    }
}