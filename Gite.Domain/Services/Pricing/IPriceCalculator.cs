using System;
using Gite.Model.Model;

namespace Gite.Model.Services.Calendar
{
    public interface IPriceCalculator
    {
        double ComputeForWeek(DateTime start);
        Price ComputeForInterval(DateTime firstWeek, DateTime lastWeek);
    }
}