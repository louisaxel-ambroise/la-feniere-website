using System.Collections.Generic;
using Gite.Model.Model;

namespace Gite.Model.Services.Calendar
{
    public interface IReservationCalendar
    {
        IEnumerable<Date> ListSaturdaysForMonth(int year, int month);
        Date GetSpecificDate(string id);
    }
}