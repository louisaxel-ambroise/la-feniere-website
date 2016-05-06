using System;

namespace Gite.Model.Business
{
    public interface IPriceStrategy
    {
        PriceResponse Calculate(PriceResponse response, DateTime dateTime);
    }
}