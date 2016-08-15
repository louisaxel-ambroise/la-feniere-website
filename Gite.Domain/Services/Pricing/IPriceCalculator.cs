using System;

namespace Gite.Model.Services.Pricing
{
    public interface IPriceCalculator
    {
        double ComputeForWeek(DateTime week);
    }
}