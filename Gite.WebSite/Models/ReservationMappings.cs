using Gite.Model.Model;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static ReservationModel MapToReservationModel(this Reservation reservation, string ip = null)
        {
            return new ReservationModel
            {
                StartsOn = reservation.FirstWeek,
                EndsOn = reservation.LastWeek.AddDays(7),
                LastWeek = reservation.LastWeek,
                FinalPrice = reservation.FinalPrice,
                OriginalPrice = reservation.DefaultPrice,
                // Reduction = reservation.ComputeDiscount(),
                Caution = 280,
                Ip = ip
            };
        }

        public static ReservationOverview MapToOverview(this Reservation reservation)
        {
            return new ReservationOverview
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn,
                StartsOn = reservation.FirstWeek,
                EndsOn = reservation.LastWeek.AddDays(7),
                LastWeek = reservation.LastWeek,
                OriginalPrice = reservation.DefaultPrice,
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