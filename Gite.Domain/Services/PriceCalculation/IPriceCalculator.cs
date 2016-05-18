using System;
using Gite.Model.Services.PriceCalculation.Strategies;

namespace Gite.Model.Services.PriceCalculation
{
    public interface IPriceCalculator
    {
        PriceResponse CalculatePrice(int year, int dayOfYear);
        PriceResponse CalculatePrice(DateTime beginDate);
    }
}