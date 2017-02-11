using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Domain.Model;
using Gite.Domain.Readers;
using Gite.Domain.Services.Pricing;

namespace Gite.Domain.Services.Calendar
{
    public class WeekCalendar : IWeekCalendar
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IPriceCalculator _priceCalculator;

        public WeekCalendar(ICalendarRepository calendarRepository, IPriceCalculator priceCalculator)
        {
            if (calendarRepository == null) throw new ArgumentNullException("bookedWeekReader");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _calendarRepository = calendarRepository;
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<Week> ListWeeksBetween(DateTime minDate, DateTime maxDate)
        {
            var result = new List<Week>();
            var start = FindFirstSaturday(minDate);

            var reservationWeeks = _calendarRepository.QueryValids();
            var bookedWeeks = reservationWeeks.Where(x => (x.FirstWeek <= minDate && x.LastWeek >= minDate) || (x.FirstWeek >= minDate && x.FirstWeek <= maxDate)).ToList();

            while (start <= maxDate)
            {
                var price = _priceCalculator.ComputeForWeek(start);
                var bookedWeek = bookedWeeks.FirstOrDefault(x => x.FirstWeek <= start && x.LastWeek >= start);
                result.Add(new Week
                {
                    ReservationId = bookedWeek != null ? bookedWeek.Id : default(Guid?),
                    Start = start,
                    IsReserved = start <= DateTime.UtcNow || bookedWeek != null,
                    IsValidated = bookedWeek != null && (bookedWeek.AdvancePaymentReceived || bookedWeek.PaymentReceived),
                    Price = price
                });

                start = start.AddDays(7);
            }

            return result;
        }

        public IEnumerable<Week> ListWeeksForMonth(int year, int month)
        {
            var date = new DateTime(year, month, 1);
            return ListWeeksBetween(date, date.AddMonths(1));
        }

        private DateTime FindFirstSaturday(DateTime start)
        {
            while (start.DayOfWeek != DayOfWeek.Saturday) start = start.AddDays(1);

            return start;
        }
    }
}