using Gite.Model.Model;
using System;

namespace Gite.Model.Services.Reservations
{
    public interface IReservationPlanner
    {
        Reservation PlanReservationForWeeks(DateTime firstWeek, DateTime lastWeek);
    }
}
