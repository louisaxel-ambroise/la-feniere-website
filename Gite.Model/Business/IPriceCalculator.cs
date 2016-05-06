namespace Gite.Model.Business
{
    public interface IPriceCalculator
    {
        PriceResponse CalculatePrice(int year, int dayOfYear);
    }
}