namespace Gite.Model.Services.PriceCalculation.Strategies
{
    public class PriceResponse
    {
        public bool Match { get; set; }
        public int Amount { get; set; }
        public bool HasDiscount { get; set; }
    }
}