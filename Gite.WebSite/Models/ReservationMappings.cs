﻿using Gite.Domain.Model;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static ReservationOverview MapToOverview(this Reservation reservation)
        {
            return new ReservationOverview
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn.AddHours(2),
                StartsOn = reservation.FirstWeek,
                EndsOn = reservation.LastWeek.AddDays(7),
                LastWeek = reservation.LastWeek,
                FinalPrice = reservation.FinalPrice,
                Caution = 280,
                Cancelled = reservation.IsCancelled,
                CancelReason = reservation.CancellationReason,

                AdvancePaymentDeclared = reservation.AdvancePaymentDeclared,
                AdvancePaymentReceived = reservation.AdvancePaymentReceived,
                AdvanceValue = reservation.AdvancePaymentValue ?? reservation.FinalPrice*0.25,
                PaymentDeclared = reservation.PaymentDeclared,
                PaymentReceived = reservation.PaymentReceived
            };
        }
    }
}