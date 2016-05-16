using Gite.Model;
using System.Linq;

namespace Gite.Database
{
    public interface IReservationRepository
    {
        void Insert(Reservation reservation);
        IQueryable<Reservation> Query();
        bool IsWeekReserved(int year, int dayOfYear);
    }
}
