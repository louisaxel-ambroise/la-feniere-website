using System.Linq;
using Gite.Model.Model;
using Gite.Model.Repositories;
using NHibernate;
using NHibernate.Linq;
using System;

namespace Gite.Database.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ISession _session;

        public ReservationRepository(ISession session)
        {
            _session = session;
        }

        public Reservation Load(Guid id)
        {
            return _session.Load<Reservation>(id);
        }

        public IQueryable<Reservation> Query()
        {
            return _session.Query<Reservation>();
        }

        public bool IsWeekReserved(DateTime firstDayOfWeek)
        {
            return _session.Query<Reservation>().Any(x => x.IsValid() && x.ContainsDate(firstDayOfWeek));
        }

        public void Insert(Reservation reservation)
        {
            _session.Save(reservation);
            _session.Flush();
        }
    }
}
