using System.Linq;
using Gite.Model.Model;
using System;

namespace Gite.Model.Repositories
{
    public interface IReservationRepository
    {
        Reservation Load(Guid reservationId);
        void Insert(Reservation reservation);
        IQueryable<Reservation> Query();
        IQueryable<Reservation> QueryValidReservations();
        bool IsWeekReserved(DateTime firstDayOfWeek);
    }
}
