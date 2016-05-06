using System;
using System.Linq;
using Gite.Model;

namespace Gite.Database
{
    public class StubReservationRepository : IReservationRepository
    {
        public Reservation Load(string id)
        {
            return new Reservation();
        }

        public IQueryable<Reservation> Query()
        {
            return new EnumerableQuery<Reservation>(new []{ new Reservation() });
        }

        public Reservation CreateReservation(int year, int dayOfYear, int price)
        {
            var startingOn = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);

            return new Reservation
            {
                Id = string.Format("{0}{1:D3}", year, dayOfYear),
                CreatedOn = DateTime.Now,
                StartingOn = startingOn,
                EndingOn = startingOn.AddDays(6),
                Confirmed = false,
                Validated = false,
                Price = price
            };
        }
    }
}