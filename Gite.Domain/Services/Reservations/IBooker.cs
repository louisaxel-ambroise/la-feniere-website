using System;
using Gite.Model.Model;

namespace Gite.Model.Services.Reservations
{
    public interface IBooker
    {
        Guid Book(DateTime firstWeek, DateTime lastWeek, ReservationDetails reservationDetails);
    }
}