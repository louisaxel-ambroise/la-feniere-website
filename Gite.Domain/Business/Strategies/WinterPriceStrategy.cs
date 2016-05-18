using System;

namespace Gite.Model.Business.Strategies
{
    public class WinterPriceStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            if(response.Match) return response;

            if (dateTime.Month == December || dateTime.Month == January)
            {
                response.Match = true;
                response.Amount = 500;
            }

            return response;
        }

        public int December = 12;
        public int January = 1;
    }
}