using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gite.Model.Services.Contract
{
    public class ParagraphBorder : PdfPageEventHelper {
        private const float Offset = 10;
        private float _startPosition;
        private int _currentCount = 0;
        private BaseFont bf;
        private PdfTemplate template;
        PdfContentByte cb;

        public bool IsBordered { get; set; }
        public int ParagraphCount { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
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
            var pageN = writer.PageNumber;
            var text = "Page " + pageN + "/";
            float len = bf.GetWidthPoint(text, 8);
            Rectangle pageSize = document.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();
            cb.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            template.BeginText();
            template.SetFontAndSize(bf, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }
    }
}