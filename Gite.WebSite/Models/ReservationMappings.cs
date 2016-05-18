using System;
using Gite.Model.Model;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static Reservation MapToReservation(this ReservationModel model, string id, string ip, Date date)
        {
            return new Reservation
            {
                Id = id,
                Ip = ip,
                StartingOn = date.BeginDate,
                EndingOn = date.EndDate,
                Price = model.Price,
                Confirmed = true,
                Validated = false,
                CreatedOn = DateTime.Now,
                Contact = new Contact
                {
                    Address = model.FormatAddress(),
                    Mail = model.Email,
                    Name = model.Name,
                    Phone = model.Phone
                }
            };
        }
    }
}