using System;

namespace Gite.Model.Services.PriceCalculation.Strategies
{
    public class SummerPriceStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            if(response.Match) return response;

            if (dateTime.Month == June)
            {
                response.Match = true;
                response.Amount = response.Caution = 400;
            }
            else if (dateTime.Month == July || dateTime.Month == August)
            {
                response.Match = true;
                response.Amount = response.Caution = 590;
            }

            return response;
        }

        public int June = 6;
        public int July = 7;
        public int August = 8;
    }
}