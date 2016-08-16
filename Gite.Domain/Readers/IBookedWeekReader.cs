using System;
using System.Linq;
using Gite.Model.Model;

namespace Gite.Model.Repositories
{
    public interface IBookedWeekReader
    {
        IQueryable<BookedWeek> QueryForReservation(Guid reservationId);
        IQueryable<BookedWeek> Query();
    }
}