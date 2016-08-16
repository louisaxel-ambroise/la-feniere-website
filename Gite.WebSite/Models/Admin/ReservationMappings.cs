using Gite.Model.Aggregates;
using Gite.Model.Views;

namespace Gite.WebSite.Models.Admin
{
    public static class ReservationMappings
    {
        public static ReservationModel MapToReservationModel(this Reservation reservation)
        {
            return new ReservationModel
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn,
                FirstWeek = reservation.FirstWeek,
                LastWeek = reservation.LastWeek,
                Name = reservation.Name,
                Mail = reservation.Mail,
                FinalPrice = reservation.FinalPrice,
            };
        }

        public static ReservationModel MapToReservationModel(this ReservationAggregate reservation)
        {
            return new ReservationModel
            {
                Id = reservation.Id,
                BookedOn = reservation.BookedOn,
                FirstWeek = reservation.FirstWeek,
                LastWeek = reservation.LastWeek,
                Name = reservation.Contact.Name,
                Address = reservation.Contact.Address,
                Mail = reservation.Contact.Mail,
                Adults = reservation.People.Adults,
                Children = reservation.People.Children,
                Babies = reservation.People.Babies,
                Animals = reservation.People.Animals,
                AnimalsType = reservation.People.AnimalsDescription,
                OriginalPrice = reservation.DefaultPrice,
                FinalPrice = reservation.FinalPrice,
                AdvancedReceived = reservation.AdvancePaymentReceived,
                AdvancedValue = reservation.AdvancePaymentValue,
                PaymentReceived = reservation.PaymentReceived,
                PaymentValue = reservation.PaymentValue

            };
        }
    }
}