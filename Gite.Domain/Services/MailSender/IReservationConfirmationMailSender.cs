using Gite.Model.Model;

namespace Gite.Model.Services.MailSender
{
    public interface IReservationConfirmationMailSender
    {
        bool ConfirmReservation(Reservation reservation);
    }
}
