using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public interface IMailSender
    {
        void Send(string address, Mail mail);
    }
}