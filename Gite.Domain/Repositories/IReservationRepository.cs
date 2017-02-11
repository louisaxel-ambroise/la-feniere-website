using System;
using System.Linq;
using Gite.Domain.Model;

namespace Gite.Domain.Readers
{
    public interface IReservationRepository
    {
        Reservation Load(Guid reservationId);
        void Insert(Reservation reservation);
        IQueryable<Reservation> Query();
        IQueryable<Reservation> QueryValids();

        void Flush();
    }
}
