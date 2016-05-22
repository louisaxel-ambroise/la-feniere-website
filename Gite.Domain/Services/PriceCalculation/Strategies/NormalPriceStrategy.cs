using System;

namespace Gite.Model.Services.PriceCalculation.Strategies
{
    public class NormalPriceStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            if (response.Match) return response;

            response.Match = true;
            response.Amount = response.Caution = 320;

            return response;
        }
    }
}