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

        public Reservation LoadByCustomId(string id)
        {
            return _session.Query<Reservation>().SingleOrDefault(x => x.CustomId == id && x.CancelToken == null);
        }

        public IQueryable<Reservation> Query()
        {
            return _session.Query<Reservation>();
        }

        public bool IsWeekReserved(int year, int dayOfYear)
        {
            var id = string.Format("{0}{1:D3}", year, dayOfYear);

            return _session.Query<Reservation>().Any(x => x.CustomId == id && x.CancelToken == null);
        }

        public void Insert(Reservation reservation)
        {
            _session.Save(reservation);
            _session.Flush();
        }
    }
}
