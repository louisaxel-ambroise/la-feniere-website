using System;
using System.Linq;
using Gite.Model.Views;

namespace Gite.Model.Readers
{
    public interface IReservationReader
    {
        Reservation Load(Guid reservationId);
        void Insert(Reservation reservation);
        IQueryable<Reservation> Query();
        IQueryable<Reservation> QueryValids();
    }
}
