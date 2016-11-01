using Gite.Model.Aggregates;
using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public interface IMailGenerator
    {
        Mail GenerateReservationCreated(ReservationAggregate reservation);
        Mail GenerateNewReservationAdmin(ReservationAggregate reservation);
        Mail GenerateAdvancePaymentDeclared(ReservationAggregate reservation);
        Mail GeneratePaymentDeclared(ReservationAggregate reservation);
        Mail GenerateReservationCancelled(ReservationAggregate reservation);

        Mail GenerateAdvancePaymentReceived(ReservationAggregate reservation);
        Mail GenerateFinalPaymentReceived(ReservationAggregate reservation);
    }
}