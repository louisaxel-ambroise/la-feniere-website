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
        private readonly IReservationWeekRepository _reservationWeekRepository;
        private readonly IPriceCalculator _priceCalculator;

        public WeekCalendar(IReservationWeekRepository reservationWeekRepository, IPriceCalculator priceCalculator)
        {
            if (reservationWeekRepository == null) throw new ArgumentNullException("reservationWeekRepository");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _reservationWeekRepository = reservationWeekRepository;
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<ReservationWeek> GetWeeksBetween(DateTime firstWeek, DateTime lastWeek)
        {
            ValidateInput(firstWeek, lastWeek);
            var weeks = new List<ReservationWeek>();

            var reservedWeeks = _reservationWeekRepository.Query().Where(x => x.StartsOn >= firstWeek && x.StartsOn <= lastWeek).ToList();

            for (var i = firstWeek; i <= lastWeek; i = i.AddDays(7))
            {
                weeks.Add(new ReservationWeek
                {
                    StartsOn = i,
                    EndsOn = i.AddDays(7),
                    IsReserved = reservedWeeks.Any(x => x.StartsOn == i) || i <= DateTime.Now.Date,
                    Price = _priceCalculator.ComputeForWeek(i)
                });
            }

            return weeks;
        }

        public IEnumerable<ReservationWeek> ListWeeksForMonth(int year, int month)
        {
            var weeks = new List<ReservationWeek>();

            var reservedWeeks = _reservationWeekRepository.Query().ToList();

            var firstSaturday = FindFirstSaturday(year, month);
            for (var i = firstSaturday; i.Month == month; i = i.AddDays(7))
            {
                weeks.Add(new ReservationWeek
                {
                    StartsOn = i,
                    EndsOn = i.AddDays(7),
                    IsReserved = reservedWeeks.Any(x => x.StartsOn == i) || i <= DateTime.Now.Date,
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

        private static void ValidateInput(DateTime firstWeek, DateTime lastWeek)
        {
            if (firstWeek.DayOfWeek != DayOfWeek.Saturday || lastWeek.DayOfWeek != DayOfWeek.Saturday)
                throw new Exception("Reservations can only start on saturday.");
        }
    }
}