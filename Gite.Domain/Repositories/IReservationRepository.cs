using System.Linq;
using Gite.Model.Model;

namespace Gite.Model.Repositories
{
    public interface IReservationRepository
    {
        void Insert(Reservation reservation);
        IQueryable<Reservation> Query();
        bool IsWeekReserved(int year, int dayOfYear);
    }
}
