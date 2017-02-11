using System;
using Gite.Domain.Model;

namespace Gite.Domain.Services.Pricing
{
    public class PriceCalculator : IPriceCalculator
    {
        public double ComputeForWeek(DateTime start)
        {
            switch (start.Month)
            {
                case 7:
                    return 590;
                case 8:
                    return (start.AddDays(7).Month > 8) ? 420 : 590;
                case 5:
                case 6:
                case 9:
                    return 420;
                default:
                    return 330;
            }
        }

        public Price ComputeForInterval(DateTime firstWeek, DateTime lastWeek)
        {
            var price = 0d;
            var reduction = ComputeReductionForInterval(firstWeek, lastWeek);
            for (var week = firstWeek; week <= lastWeek; week = week.AddDays(7))
            {
                price += ComputeForWeek(week);
            }

            return new Price
            {
                Original = price,
                Reduction = reduction,
                Final = Math.Ceiling(price - (price*reduction / 100))
            };
        }

        private static int ComputeReductionForInterval(DateTime firstWeek, DateTime lastWeek)
        {
            var numberOfWeeks = (lastWeek.AddDays(7) - firstWeek).Days/7;

            if (numberOfWeeks == 2) return 3;
            if (numberOfWeeks > 2) return 4;

            return 0;
        }
    }
}