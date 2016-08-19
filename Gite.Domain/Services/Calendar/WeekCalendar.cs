using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Model.Model;
using Gite.Model.Readers;
using Gite.Model.Services.Pricing;

namespace Gite.Model.Services.Calendar
{
    public class WeekCalendar : IWeekCalendar
    {
        private readonly IBookedWeekReader _bookedWeekReader;
        private readonly IPriceCalculator _priceCalculator;

        public WeekCalendar(IBookedWeekReader bookedWeekReader, IPriceCalculator priceCalculator)
        {
            if (bookedWeekReader == null) throw new ArgumentNullException("bookedWeekReader");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _bookedWeekReader = bookedWeekReader;
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<Week> ListWeeksForMonth(int year, int month)
        {
            var result = new List<Week>();
            var start = FindFirstSaturday(year, month);

            var bookedWeeks = _bookedWeekReader.QueryValids().Where(x => x.Week >= start && x.Week < new DateTime(year, month, 1).AddMonths(1)).ToList();

            while (start.Month == month)
            {
                var price = _priceCalculator.ComputeForWeek(start);

                result.Add(new Week
                {
                    Start = start,
                    IsReserved = start <= DateTime.UtcNow || bookedWeeks.Any(x => x.Week == start),
                    Price = price
                });

                start = start.AddDays(7);
            }

            return result;
        }

        private DateTime FindFirstSaturday(int year, int month)
        {
            var start = new DateTime(year, month, 1);

            while (start.DayOfWeek != DayOfWeek.Saturday) start = start.AddDays(1);

            return start;
        }
    }
}