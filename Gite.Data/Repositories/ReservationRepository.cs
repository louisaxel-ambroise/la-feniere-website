using System.Linq;
using Gite.Model.Model;
using Gite.Model.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Gite.Database.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ISession _session;

        public ReservationRepository(ISession session)
        {
            _session = session;
        }

        public Reservation Load(string id)
        {
            return _session.Load<Reservation>(id);
        }

        public IQueryable<Reservation> Query()
        {
            return _session.Query<Reservation>();
        }

        public bool IsWeekReserved(int year, int dayOfYear)
        {
            var id = string.Format("{0}{1:D3}", year, dayOfYear);
            var resa = _session.Query<Reservation>().FirstOrDefault(x => x.Id == id);

            return resa != null;
        }

        public void Insert(Reservation reservation)
        {
            _session.Save(reservation);
        }
    }
}
