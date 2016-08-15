using System.IO;

namespace Gite.Model.Model
{
    public class Mail
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Stream[] Attachments { get; internal set; }
    }
}
