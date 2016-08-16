using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public interface IMailSender
    {
        string From { get; }
        void SendMail(Mail message, string address);
    }
}