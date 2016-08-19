using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gite.Model.Services.Contract
{
    public class ParagraphBorder : PdfPageEventHelper {
        private const float Offset = 10;
        private float _startPosition;
        private int _currentCount = 0;

        public bool IsBordered { get; set; }
        public int ParagraphCount { get; set; }

        public override void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
            _currentCount++;
            _startPosition = paragraphPosition;
        }

        public override void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) {
            if (!IsBordered || _currentCount != ParagraphCount) return;

            var cb = writer.DirectContentUnder;
            cb.Rectangle(document.Left-10, paragraphPosition - Offset, document.Right - document.Left+10, _startPosition - paragraphPosition + Offset);
            cb.Stroke();
            _currentCount = 0;
        }
    }
}