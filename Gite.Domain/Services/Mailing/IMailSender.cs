using Gite.Domain.Model;

namespace Gite.Domain.Services.Mailing
{
    public interface IMailSender
    {
        void SendReservationCreated(Reservation reservation);
        void SendAdvancePaymentDeclared(Reservation reservation);
        void SendPaymentDeclared(Reservation reservation);
        void SendReservationCancelled(Reservation reservation);
        void SendAdvancePaymentReceived(Reservation reservation);
        void SendFinalPaymentReceived(Reservation reservation);
    }
}