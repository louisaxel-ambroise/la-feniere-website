﻿using Gite.Model;
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

        public Reservation CreateReservation(int year, int dayOfYear, int price)
        {
            var startingOn = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);

            var reservation = new Reservation
            {
                Id = string.Format("{0}{1:D3}", year, dayOfYear),
                CreatedOn = DateTime.Now,
                StartingOn = startingOn,
                EndingOn = startingOn.AddDays(6),
                Confirmed = false,
                Validated = false,
                Price = price
            };

            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(reservation);

                transaction.Commit();
            }

            return reservation;
        }
    }
}
