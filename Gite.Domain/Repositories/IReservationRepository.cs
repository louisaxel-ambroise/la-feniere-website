using System.Linq;
using Gite.Model.Model;
using System;

namespace Gite.Model.Repositories
{
    public interface IReservationRepository
    {
        Reservation Load(Guid reservationId);
        Reservation LoadByCustomId(string reservationId);
        void Insert(Reservation reservation);
        IQueryable<Reservation> Query();
        bool IsWeekReserved(int year, int dayOfYear);
    }
}
