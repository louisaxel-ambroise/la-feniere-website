using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Model.Dtos;
using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services.Pricing;

namespace Gite.Model.Services.Calendar
{
    public class WeekWeekCalendar : IWeekCalendar
    {
        private readonly IReservationWeekRepository _reservationWeekRepository;
        private readonly IPriceCalculator _priceCalculator;

        public WeekWeekCalendar(IReservationWeekRepository reservationWeekRepository, IPriceCalculator priceCalculator)
        {
            if (reservationWeekRepository == null) throw new ArgumentNullException("reservationWeekRepository");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _reservationWeekRepository = reservationWeekRepository;
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<WeekDetail> GetWeeksBetween(DateTime firstWeek, DateTime lastWeek)
        {
            ValidateInput(firstWeek, lastWeek);
            var weeks = new List<WeekDetail>();

            var reservedWeeks = _reservationWeekRepository.Query().Where(x => x.StartsOn >= firstWeek && x.StartsOn <= lastWeek).ToList();

            for (var i = firstWeek; i <= lastWeek; i = i.AddDays(7))
            {
                weeks.Add(new WeekDetail
                {
                    StartsOn = i,
                    IsReserved = reservedWeeks.Any(x => x.StartsOn == i),
                    Price = _priceCalculator.ComputeForWeek(i)
                });
            }

            return weeks;
        }

        private static void ValidateInput(DateTime firstWeek, DateTime lastWeek)
        {
            if (firstWeek.DayOfWeek != DayOfWeek.Saturday || lastWeek.DayOfWeek != DayOfWeek.Saturday)
                throw new Exception("Reservations can only start on saturday.");
        }
    }
}