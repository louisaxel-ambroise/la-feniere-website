using System.Collections.Generic;
using Gite.Domain.Model;
using System;

namespace Gite.Domain.Services.Calendar
{
    public interface IWeekCalendar
    {
        IEnumerable<Week> ListWeeksBetween(DateTime minDate, DateTime maxDate);
        IEnumerable<Week> ListWeeksForMonth(int year, int month);
    }
}