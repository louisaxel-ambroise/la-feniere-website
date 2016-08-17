using System;
using System.Linq;
using Gite.Model.Model;
using Gite.Model.Readers;
using NHibernate;
using NHibernate.Linq;

namespace Gite.Database.Repositories
{
    public class ReservationReader : IReservationReader
    {
        private readonly ISession _session;

        public ReservationReader(ISession session)
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
            return Query().Where(x => !x.IsCancelled && (x.AdvancePaymentReceived || x.BookedOn > DateTime.Now.AddDays(-5)));
        }

        public void Insert(Reservation reservation)
        {
            _session.Save(reservation);
            _session.Flush();
        }
    }
}
