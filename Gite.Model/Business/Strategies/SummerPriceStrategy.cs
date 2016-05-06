using System;

namespace Gite.Model.Business
{
    public class SummerPriceStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            if(response.Match) return response;

            if (dateTime.Month == June)
            {
                response.Match = true;
                response.Amount = 400;
            }
            else if (dateTime.Month == July || dateTime.Month == August)
            {
                response.Match = true;
                response.Amount = 590;
            }

            return response;
        }

        public int June = 6;
        public int July = 7;
        public int August = 8;
    }
}