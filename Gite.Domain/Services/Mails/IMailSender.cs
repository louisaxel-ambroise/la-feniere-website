using Gite.Model.Model;

namespace Gite.Model.Services.Mails
{
    public interface IMailSender
    {
        void SendMail(Mail mail, string address);
    }
}