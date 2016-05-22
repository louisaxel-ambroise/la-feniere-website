using System;
using Gite.Model.Model;
using Gite.WebSite.Models.Api;

namespace Gite.WebSite.Models
{
    public static class ReservationMappings
    {
        public static Reservation MapToReservation(this ReservationModel model, string id, string ip, Date date)
        {
            return new Reservation
            {
                CustomId = id,
                Ip = ip,
                StartingOn = date.BeginDate,
                EndingOn = date.EndDate,
                Price = model.Price,
                Caution = model.Caution,
                CautionRefunded = true,
                PaymentReceived = false,
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

        public static ApiReservation MapToApiReservation(this Reservation reservation)
        {
            return new ApiReservation
            {
                Id = reservation.Id,
                CustomId = reservation.CustomId,
                StartingOn = reservation.StartingOn.ToString("yyyy-MM-dd"),
                EndingOn = reservation.EndingOn.ToString("yyyy-MM-dd"),
                Mail = reservation.Contact.Mail,
                Name = reservation.Contact.Name,
                PaymentReceived = reservation.PaymentReceived,
                CautionRefunded = reservation.CautionRefunded,
                Price = reservation.Price,
                Caution = reservation.Caution
            };
        }
    }
}