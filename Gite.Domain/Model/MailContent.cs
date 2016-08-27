using System.Collections.Generic;

namespace Gite.Model.Model
{
    public class MailContent
    {
        public string Content { get; set; }
        public bool IsHtml { get; set; }
        public IEnumerable<MailAttachment> Attachments { get; set; }

        public MailContent()
        {
            Attachments = new List<MailAttachment>();
        }
    }
}