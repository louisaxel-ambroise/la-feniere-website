using Gite.Model.Aggregates;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static ReservationOverview MapToOverview(this ReservationAggregate reservation)
        {
            return new ReservationOverview
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn,
                StartsOn = reservation.FirstWeek,
                EndsOn = reservation.LastWeek.AddDays(7),
                LastWeek = reservation.LastWeek,
                FinalPrice = reservation.FinalPrice,
                Caution = 280,
                Cancelled = reservation.IsCancelled,
                CancelReason = reservation.CancellationReason,

                AdvancePaymentDeclared = reservation.AdvancePaymentDeclared,
                AdvancePaymentReceived = reservation.AdvancePaymentReceived,
                AdvanceValue = reservation.AdvancePaymentValue ?? reservation.FinalPrice*0.25
            };
        }
    }
}