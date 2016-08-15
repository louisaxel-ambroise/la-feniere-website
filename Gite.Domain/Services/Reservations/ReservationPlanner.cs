using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services.Pricing;
using System;
using System.Linq;

namespace Gite.Model.Services.Reservations
{
    public class ReservationPlanner : IReservationPlanner
    {
        private static IPriceCalculator _priceCalculator;
        private static IReservationRepository _reservationRepository;

        public ReservationPlanner(IReservationRepository reservationRepository, IPriceCalculator priceCalculator)
        {
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _priceCalculator = priceCalculator;
            _reservationRepository = reservationRepository;
        }

        public bool ContainsBookedWeek(DateTime firstWeek, DateTime lastWeek)
        {
            // Check if any reservation : starts leq firstWeek and ends geq lastWeek
            //                            starts leq lastWeek  and ends geq lastWeek
            //                            starts geq firstWeek and starts leq lastWeek

            return _reservationRepository.QueryValidReservations()
                    .Any(x =>   x.FirstWeek <= firstWeek && x.LastWeek >= lastWeek
                             || x.FirstWeek <= lastWeek && x.LastWeek >= lastWeek
                             || x.FirstWeek >= firstWeek && x.FirstWeek <= lastWeek);
        }

        public Reservation PlanReservationForWeeks(DateTime firstWeek, DateTime lastWeek)
        {
            var price = 0d;
            for(var i=firstWeek ; i<=lastWeek ; i=i.AddDays(7)) price += _priceCalculator.ComputeForWeek(i);

            var reservation = new Reservation
            {
                FirstWeek = firstWeek,
                LastWeek = lastWeek,
                BookedOn = DateTime.Now
            };

            var reduction = reservation.ComputeDiscount();
            reservation.DefaultPrice = price;
            reservation.FinalPrice = Math.Ceiling(price - (price * reduction / 100));

            return reservation;
        }
    }
}
