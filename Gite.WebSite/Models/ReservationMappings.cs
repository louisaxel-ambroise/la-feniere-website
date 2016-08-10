using System;
using Gite.Model.Model;
using Gite.WebSite.Models.Api;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static Reservation MapToReservation(this ReservationModel model, string id, string ip)
        {
            return new Reservation
            {
                Contact = new Contact
                {
                    Address = model.FormatAddress(),
                    Mail = model.Email,
                    Name = model.Name,
                    Phone = model.Phone
                }
            };
        }

        public static ReservationOverview MapToOverview(this Reservation reservation)
        {
            return new ReservationOverview
            {

            };
        }

        public static ApiReservation MapToApiReservation(this Reservation reservation)
        {
            return new ApiReservation
            {
                Id = reservation.Id,
            };
        }
    }
}