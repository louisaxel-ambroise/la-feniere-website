using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gite.Domain.Services.Contract
{
    public class ParagraphBorder : PdfPageEventHelper {
        private const float Offset = 10;
        private float _startPosition;
        private int _currentCount = 0;
        private BaseFont bf;
        private PdfTemplate template;

        public bool IsBordered { get; set; }
        public int ParagraphCount { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            template = writer.DirectContent.CreateTemplate(100, 100);
            template.BoundingBox = new Rectangle(-20, -20, 100, 100);
            bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        }

        public override void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
            if (_currentCount == 0) _startPosition = paragraphPosition;

            if (IsBordered) _currentCount++;
            else _currentCount = 0;
            
        }

        public override void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) {
            if (!IsBordered || _currentCount != ParagraphCount) return;

            var cb = writer.DirectContentUnder;
            cb.Rectangle(document.Left-10, paragraphPosition - Offset, document.Right - document.Left+10, _startPosition - paragraphPosition + Offset);
            cb.Stroke();
            _currentCount = 0;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // http://stackoverflow.com/questions/1032614/itextsharp-creating-a-footer-page-of
            base.OnEndPage(writer, document);

            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            string text = "" + writer.PageNumber + "/";
            float textBase = document.Bottom - 20;
            float textSize = 12; //helv.GetWidthPoint(text, 12);
            cb.BeginText();
            cb.SetFontAndSize(bf, 12);
            cb.SetTextMatrix(document.Left, textBase);
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(template, document.Left + textSize, textBase);
            cb.RestoreState();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            template.BeginText();
            template.SetFontAndSize(bf, 12);
            template.SetTextMatrix(0, 0);
            int pageNumber = writer.PageNumber;
            template.ShowText(Convert.ToString(pageNumber));
            template.EndText();
        }
    }
}