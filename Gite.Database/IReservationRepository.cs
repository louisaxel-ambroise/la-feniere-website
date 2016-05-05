using Gite.Model;
using System.Linq;

namespace Gite.Database
{
    public interface IReservationRepository
    {
        Reservation Load(string id);
        IQueryable<Reservation> Query();
    }
}
