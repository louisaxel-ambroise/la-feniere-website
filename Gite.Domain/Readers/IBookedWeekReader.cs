﻿using System;
using System.Linq;
using Gite.Model.Model;

namespace Gite.Model.Readers
{
    public interface IBookedWeekReader
    {
        IQueryable<BookedWeek> Query();
        IQueryable<BookedWeek> QueryValids();
        IQueryable<BookedWeek> QueryForReservation(Guid reservationId);
    }
}