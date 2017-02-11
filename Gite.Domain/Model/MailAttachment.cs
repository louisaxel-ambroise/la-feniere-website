using System.IO;

namespace Gite.Domain.Model
{
    public class MailAttachment
    {
        public string Name { get; set; }
        public Stream Data { get; set; }
    }
}