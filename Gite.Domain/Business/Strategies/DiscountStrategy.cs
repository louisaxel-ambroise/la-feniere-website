using System;

namespace Gite.Model.Business.Strategies
{
    public class DiscountStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            var offset = dateTime - DateTime.Now.Date;
            
            if (offset.Days < 7)
            {
                // Apply discount -> 10% (reservation last minute)
                response.Amount = ApplyDiscount(response.Amount);
                response.HasDiscount = true;
            }

            return response;
        }

        private int ApplyDiscount(int price)
        {
            return (price - (price/10));
        }
    }
}