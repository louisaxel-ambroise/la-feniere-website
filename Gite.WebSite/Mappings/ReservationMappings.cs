using Gite.Domain.Model;

namespace Gite.WebSite.Models.Admin
{
    public static class AdminReservationMappings
    {
        public static ReservationModel MapToReservationModel(this Reservation reservation)
        {
            return new ReservationModel
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn.AddHours(2),
                FirstWeek = reservation.FirstWeek,
                LastWeek = reservation.LastWeek,
                Name = reservation.Contact.Name,
                Mail = reservation.Contact.Mail,
                Phone = reservation.Contact.Phone,
                FinalPrice = reservation.FinalPrice,
                LastMinute = (reservation.FirstWeek - reservation.BookedOn.Date).Days <= 7,
                PaymentReceived = reservation.PaymentReceived,
                AdvancedReceived = reservation.AdvancePaymentReceived
            };
        }

        public static ReservationModel MapToDetailedReservationModel(this Reservation reservation)
        {
            return new ReservationModel
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn.AddHours(2),
                FirstWeek = reservation.FirstWeek,
                LastWeek = reservation.LastWeek,
                LastMinute = reservation.IsLastMinute,

                Name = reservation.Contact.Name,
                Address = reservation.Contact.Address,
                Mail = reservation.Contact.Mail,
                Phone = reservation.Contact.Phone,
                Adults = reservation.People.Adults,
                Children = reservation.People.Children,
                Babies = reservation.People.Babies,
                Animals = reservation.People.Animals,
                AnimalsType = reservation.People.AnimalsDescription,
                OriginalPrice = reservation.DefaultPrice,
                FinalPrice = reservation.FinalPrice,
                AdvancedReceived = reservation.AdvancePaymentReceived,
                AdvancedValue = reservation.IsLastMinute ? 0 : reservation.AdvancePaymentValue,
                PaymentReceived = reservation.PaymentReceived,
                PaymentValue = reservation.PaymentValue
            };
        }
    }
}