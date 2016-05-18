using System;
using System.Collections.Generic;
using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services.PriceCalculation;

namespace Gite.Model.Services.Calendar
{
    public class ReservationCalendar : IReservationCalendar
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPriceCalculator _priceCalculator;

        public ReservationCalendar(IReservationRepository reservationRepository, IPriceCalculator priceCalculator)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _reservationRepository = reservationRepository;
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<Date> ListSaturdaysForMonth(int year, int month)
        {
            var firstDate = FirstSaturday(new DateTime(year, month, 1));

            for (var day = firstDate; day.Month == month; day = day.AddDays(7))
            {
                var date = CreateDate(day);

                yield return date;
            }
        }

        public Date GetSpecificDate(string id)
        {
            var year = int.Parse(id.Substring(0, 4));
            var dayOfYear = int.Parse(id.Substring(4, 3));
            var dateTime = new DateTime(year, 1, 1).AddDays(dayOfYear-1);

            return CreateDate(dateTime);
        }

        private Date CreateDate(DateTime beginDate)
        {
            var date = new Date(beginDate);

            var isReserved = _reservationRepository.IsWeekReserved(date.Year, date.DayOfYear);
            var calculatedPrice = _priceCalculator.CalculatePrice(date.Year, date.DayOfYear);

            date.Reserved = date.Reserved || isReserved;
            date.Price = calculatedPrice;

            return date;
        }

        private static DateTime FirstSaturday(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Saturday) date = date.AddDays(1);

            return date;
        }
    }
}