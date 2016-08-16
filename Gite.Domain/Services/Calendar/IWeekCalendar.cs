using System.Collections.Generic;
using Gite.Model.Model;

namespace Gite.Model.Services.Calendar
{
    public interface IWeekCalendar
    {
        IEnumerable<Week> ListWeeksForMonth(int year, int month);
    }
}