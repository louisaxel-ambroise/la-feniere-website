using System;
using Gite.Model.Model;

namespace Gite.Model.Services.Reservations
{
    public interface IBooker
    {
        Guid Book(Reservation reservation, ReservationDetails reservationDetails);
    }
}