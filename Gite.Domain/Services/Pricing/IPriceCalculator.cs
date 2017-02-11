using System;
using Gite.Domain.Model;

namespace Gite.Domain.Services.Pricing
{
    public interface IPriceCalculator
    {
        double ComputeForWeek(DateTime start);
        Price ComputeForInterval(DateTime firstWeek, DateTime lastWeek);
    }
}