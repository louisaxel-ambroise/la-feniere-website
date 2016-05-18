using System;

namespace Gite.Model.Business.Strategies
{
    public class NormalPriceStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            if (response.Match) return response;

            response.Match = true;
            response.Amount = 320;

            return response;
        }
    }
}