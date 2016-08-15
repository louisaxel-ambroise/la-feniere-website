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
                Reduction = reservation.ComputeDiscount(),
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
                Reduction = reservation.ComputeDiscount(),
                OriginalPrice = reservation.DefaultPrice,
                FinalPrice = reservation.FinalPrice,
                PaymentDeclared = reservation.PaymentDeclarationDate.HasValue,
                PaymentReceived = reservation.PaymentReceptionDate.HasValue,
                Caution = 280,
                Cancelled = reservation.CancellationToken.HasValue,
                CancelReason = FormatCancelReason(reservation.CancellationReason),
                CancelledOn = reservation.CancelledOn,

                AdvancePaymentDeclared = reservation.AdvancedDeclarationDate.HasValue,
                AdvancePaymentReceived = reservation.AdvancedReceptionDate.HasValue,
                AdvanceValue = reservation.AdvancedValue ?? reservation.FinalPrice*0.25
            };
        }

        private static string FormatCancelReason(CancelReason? reason)
        {
            if(reason == null)
                    return "La rison de l'annulation n'est pas connue.";

            switch (reason)
            {
                case CancelReason.AdvanceNotReceived:
                    return "La caution n'a pas été reçue à temps (au plus tard 5 jours après la réservation).";
                case CancelReason.CancelledByOwner:
                    return "Le propriétaire a annulé la réservation.";
                case CancelReason.CancelledByUser:
                    return "Vous avez annulé la réservation.";
                case CancelReason.PaymentNotReceived:
                    return "Le paiement n'a pas été reçu à temps (au plus tard 10 jours avant le début de la location).";
                default:
                    return "La rison de l'annulation n'est pas connue.";
            }
        }
    }
}