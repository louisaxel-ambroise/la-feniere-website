using System;
using System.Collections.Generic;
using Gite.Model.Model;

namespace Gite.Model.Services.Calendar
{
    public interface IWeekCalendar
    {
        IEnumerable<ReservationWeek> GetWeeksBetween(DateTime firstWeek, DateTime lastWeek);
        IEnumerable<ReservationWeek> ListWeeksForMonth(int year, int month);
    }
}