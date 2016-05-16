using Gite.Model;
using System.Linq;
using System;
using NHibernate;
using NHibernate.Linq;

namespace Gite.Database
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
            var resa = _session.Query<Reservation>()
                .SingleOrDefault(x => 
                    x.Id == string.Format("{0}{1:D3}", year, dayOfYear) 
                    && (x.Validated || x.CreatedOn >= DateTime.Now.AddMinutes(-30))
                );

            return resa != null;
        }

        public void Insert(Reservation reservation)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(reservation);

                transaction.Commit();
            }
        }
    }
}
