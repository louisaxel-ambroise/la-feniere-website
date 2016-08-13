using System;

namespace Gite.Model.Services.Pricing
{
    public class PriceCalculator : IPriceCalculator
    {
        public double ComputeForWeek(DateTime week)
        {
            switch (week.Month)
            {
                case 7:
                    return 590;
                case 8:
                    return (week.AddDays(7).Month > 8) ? 420 : 590;
                case 5:
                case 6:
                case 9:
                    return 420;
                default:
                    return 330;
            }
        }
    }
}