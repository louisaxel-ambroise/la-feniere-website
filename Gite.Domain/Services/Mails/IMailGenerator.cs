using Gite.Model.Model;

namespace Gite.Model.Services.Mails
{
    public interface IMailGenerator
    {
        Mail GenerateMail(Reservation reservation);
    }
}