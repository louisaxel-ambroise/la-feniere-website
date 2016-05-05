using Gite.Model;
using System.Linq;
using System;

namespace Gite.Database
{
    public class ReservationRepository : IReservationRepository
    {
        public Reservation Load(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> Query()
        {
            throw new NotImplementedException();
        }
    }
}
