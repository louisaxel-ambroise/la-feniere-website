using System;

namespace Gite.Model.Business.Strategies
{
    public interface IPriceStrategy
    {
        PriceResponse Calculate(PriceResponse response, DateTime dateTime);
    }
}