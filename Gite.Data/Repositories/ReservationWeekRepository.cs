using System;
using System.Linq;
using Gite.Model.Model;
using Gite.Model.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Gite.Database.Repositories
{
    public class ReservationWeekRepository : IReservationWeekRepository
    {
        private readonly ISession _session;

        public ReservationWeekRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
        }

        public void Insert(ReservationWeek reservation)
        {
            _session.Save(reservation);
        }

        public bool IsWeekReserved(DateTime firstDayOfWeek)
        {
            return _session.Query<ReservationWeek>().Any(x => x.StartsOn == firstDayOfWeek && !x.IsCancelled());
        }

        public ReservationWeek Load(Guid reservationId)
        {
            return _session.Load<ReservationWeek>(reservationId);
        }

        public IQueryable<ReservationWeek> Query()
        {
            return _session.Query<ReservationWeek>();
        }
    }
}
