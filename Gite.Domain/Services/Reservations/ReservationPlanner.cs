using Gite.Model.Model;
using Gite.Model.Services.Calendar;
using System;
using System.Linq;

namespace Gite.Model.Services.Reservations
{
    public class ReservationPlanner : IReservationPlanner
    {
        private static IWeekCalendar _weekCalendar;

        public ReservationPlanner(IWeekCalendar weekCalendar)
        {
            if (weekCalendar == null) throw new ArgumentNullException("weekCalendar");

            _weekCalendar = weekCalendar;
        }

        public Reservation PlanReservationForWeeks(DateTime firstWeek, DateTime lastWeek)
        {
            var reservation = new Reservation();
            reservation.Weeks = _weekCalendar.GetWeeksBetween(firstWeek, lastWeek).ToList();

            return reservation;
        }
    }
}
