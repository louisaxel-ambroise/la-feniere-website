using System;
using Gite.Model.Readers;
using Gite.Model.Views;
using NHibernate;

namespace Gite.Database.Repositories
{
    public class BookedWeekRepository : IBookedWeekRepository
    {
        private readonly ISession _session;

        public BookedWeekRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
        }

        public void Add(BookedWeek week)
        {
            _session.Save(week);
        }
    }
}