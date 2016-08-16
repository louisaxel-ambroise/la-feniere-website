using Gite.Model.Model;

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
            };
        }
    }
}