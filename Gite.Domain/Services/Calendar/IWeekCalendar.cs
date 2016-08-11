using System;
using System.Collections.Generic;
using Gite.Model.Dtos;

namespace Gite.Model.Services.Calendar
{
    public interface IWeekCalendar
    {
        IEnumerable<WeekDetail> GetWeeksBetween(DateTime firstWeek, DateTime lastWeek);
    }
}