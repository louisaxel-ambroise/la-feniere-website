using Gite.Domain.Model;

namespace Gite.Domain.Services.Mailing
{
    public interface IMailGenerator
    {
        Mail GenerateReservationCreated(Reservation reservation);
        Mail GenerateNewReservationAdmin(Reservation reservation);
        Mail GenerateAdvancePaymentDeclared(Reservation reservation);
        Mail GeneratePaymentDeclared(Reservation reservation);
        Mail GenerateReservationCancelled(Reservation reservation);

        Mail GenerateAdvancePaymentReceived(Reservation reservation);
        Mail GenerateFinalPaymentReceived(Reservation reservation);
    }
}