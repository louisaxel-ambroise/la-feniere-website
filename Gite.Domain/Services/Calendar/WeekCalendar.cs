using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Model.Repositories;
using Gite.Model.Services.Pricing;
using Gite.Model.Model;

namespace Gite.Model.Services.Calendar
{
    public class WeekCalendar : IWeekCalendar
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPriceCalculator _priceCalculator;

        public WeekCalendar(IReservationRepository reservationRepository, IPriceCalculator priceCalculator)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _reservationRepository = reservationRepository;
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<ReservationWeek> GetWeeksBetween(DateTime firstWeek, DateTime lastWeek)
        {
            ValidateInput(firstWeek, lastWeek);
            var weeks = new List<ReservationWeek>();

            for (var i = firstWeek; i <= lastWeek; i = i.AddDays(7))
            {
                weeks.Add(new ReservationWeek
                {
                    StartsOn = i,
                    EndsOn = i.AddDays(7),
                    IsReserved = IsWeekBooked(i),
                    Price = _priceCalculator.ComputeForWeek(i)
                });
            }

            return weeks;
        }

        public IEnumerable<ReservationWeek> ListWeeksForMonth(int year, int month)
        {
            var weeks = new List<ReservationWeek>();
            var firstSaturday = FindFirstSaturday(year, month);

            for (var i = firstSaturday; i.Month == month; i = i.AddDays(7))
            {
                weeks.Add(new ReservationWeek
                {
                    StartsOn = i,
                    EndsOn = i.AddDays(7),
                    IsReserved = IsWeekBooked(i),
                    Price = _priceCalculator.ComputeForWeek(i)
                });
            }

            return weeks;
        }

        private static DateTime FindFirstSaturday(int year, int month)
        {
            var date = new DateTime(year, month, 1);
            while (date.DayOfWeek != DayOfWeek.Saturday) date = date.AddDays(1);

            return date;
        }

        private bool IsWeekBooked(DateTime firstWeek)
        {
            return firstWeek <= DateTime.Now.Date || _reservationRepository.QueryValidReservations().Any(x => x.FirstWeek <= firstWeek && x.LastWeek >= firstWeek);
        }

        private static void ValidateInput(params DateTime[] dates)
        {
            if (dates.Any(x => x.DayOfWeek != DayOfWeek.Saturday)) throw new Exception("Reservations can only start on saturday.");
        }
    }
}