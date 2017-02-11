using Gite.Domain.Model;
using System.Linq;

namespace Gite.Domain.Readers
{
    public interface ICalendarRepository
    {
        IQueryable<ReservationCalendar> QueryValids();
    }
}
