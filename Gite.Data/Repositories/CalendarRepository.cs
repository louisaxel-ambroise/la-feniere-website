using System;
using System.Linq;
using Gite.Domain.Readers;
using NHibernate;
using NHibernate.Linq;
using Gite.Domain.Model;

namespace Gite.Database.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly ISession _session;

        public CalendarRepository(ISession session)
        {
            if (session == null) throw new ArgumentException("session");

            _session = session;
        }

        public IQueryable<ReservationCalendar> QueryValids()
        {
            return _session.Query<ReservationCalendar>().Where(x => (!x.IsCancelled && x.DisablesOn >= DateTime.UtcNow) || x.AdvancePaymentReceived || x.AdvancePaymentReceived);
        }
    }
}
