using System;
using System.Linq;
using Gite.Model.Model;

namespace Gite.Model.Repositories
{
    public interface IReservationWeekRepository
    {
        ReservationWeek Load(Guid reservationId);
        void Insert(ReservationWeek reservation);
        IQueryable<ReservationWeek> Query();
        bool IsWeekReserved(DateTime firstDayOfWeek); 
    }
}