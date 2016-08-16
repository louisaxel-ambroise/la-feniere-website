using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public interface IMailSender
    {
        void SendMail(Mail message, string address);
    }
}