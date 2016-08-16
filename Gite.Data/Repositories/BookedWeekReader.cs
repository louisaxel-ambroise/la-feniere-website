using System;
using System.Linq;
using Gite.Model.Readers;
using Gite.Model.Views;
using NHibernate;
using NHibernate.Linq;

namespace Gite.Database.Repositories
{
    public class BookedWeekReader : IBookedWeekReader
    {
        private readonly ISession _session;

        public BookedWeekReader(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
        }

        public IQueryable<BookedWeek> Query()
        {
            return _session.Query<BookedWeek>();
        }

        public IQueryable<BookedWeek> QueryValids()
        {
            return Query().Where(x => !x.Cancelled && (x.DisablesOn == null || x.DisablesOn > DateTime.Now));
        }

        public IQueryable<BookedWeek> QueryForReservation(Guid reservationId)
        {
            return _session.Query<BookedWeek>().Where(x => x.ReservationId == reservationId);
        }
    }
}