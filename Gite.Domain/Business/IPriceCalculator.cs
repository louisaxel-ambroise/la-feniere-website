using System;
using Gite.Model.Business.Strategies;

namespace Gite.Model.Business
{
    public interface IPriceCalculator
    {
        PriceResponse CalculatePrice(int year, int dayOfYear);
        PriceResponse CalculatePrice(DateTime beginDate);
    }
}