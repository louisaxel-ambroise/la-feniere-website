using System;

namespace Gite.Model.Services.PriceCalculation.Strategies
{
    public class DiscountStrategy : IPriceStrategy
    {
        public PriceResponse Calculate(PriceResponse response, DateTime dateTime)
        {
            var offset = dateTime - DateTime.Now.Date;
            
            if (offset.Days < 7)
            {
                response.Amount = ApplyDiscount(response.Amount); // Apply discount -> 10% (reservation last minute)
                response.HasDiscount = true;
            }

            return response;
        }

        private float ApplyDiscount(float price)
        {
            return (price - (price/10));
        }
    }
}