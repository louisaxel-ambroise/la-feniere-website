using System;
using System.Linq;
using Gite.Domain.Model;
using Gite.Domain.Readers;
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

        public Reservation Load(Guid id)
        {
            return _session.Load<Reservation>(id);
        }

        public IQueryable<Reservation> Query()
        {
            return _session.Query<Reservation>();
        }

        public IQueryable<Reservation> QueryValids()
        {
            return Query().Where(x => !x.IsCancelled && (x.AdvancePaymentReceived || x.DisablesOn > DateTime.UtcNow));
        }

        public void Insert(Reservation reservation)
        {
            _session.Save(reservation);
        }

        public void Flush()
        {
            _session.Flush();
        }
    }
}
