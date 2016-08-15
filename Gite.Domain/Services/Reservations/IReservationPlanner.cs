using Gite.Model.Model;
using System;

namespace Gite.Model.Services.Reservations
{
    public interface IReservationPlanner
    {
        bool ContainsBookedWeek(DateTime firstWeek, DateTime lastWeek);
        Reservation PlanReservationForWeeks(DateTime firstWeek, DateTime lastWeek);
    }
}
