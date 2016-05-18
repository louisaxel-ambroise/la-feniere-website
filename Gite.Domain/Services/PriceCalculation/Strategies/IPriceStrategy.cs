using System;

namespace Gite.Model.Services.PriceCalculation.Strategies
{
    public interface IPriceStrategy
    {
        PriceResponse Calculate(PriceResponse response, DateTime dateTime);
    }
}