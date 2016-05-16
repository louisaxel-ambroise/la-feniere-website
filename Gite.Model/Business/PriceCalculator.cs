﻿using System;
using System.Linq;

namespace Gite.Model.Business
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IPriceStrategy[] _strategies;

        public PriceCalculator()
        {
            _strategies = new IPriceStrategy[]
            {
                new SummerPriceStrategy(), new WinterPriceStrategy(), new NormalPriceStrategy(), new DiscountStrategy()
            };
        }

        public PriceResponse CalculatePrice(int year, int dayOfYear)
        {
            var date = new DateTime(year, 1, 1).AddDays(dayOfYear -1);

            return CalculatePrice(date);
        }

        public PriceResponse CalculatePrice(DateTime beginDate)
        {
            var response = new PriceResponse();

            response = _strategies.Aggregate(response, (current, strategy) => strategy.Calculate(current, beginDate));

            return response;
        }
    }
}