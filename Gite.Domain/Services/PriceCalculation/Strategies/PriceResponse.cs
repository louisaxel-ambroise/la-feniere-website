namespace Gite.Model.Services.PriceCalculation.Strategies
{
    public class PriceResponse
    {
        public bool Match { get; set; }
        public float Amount { get; set; }
        public float Caution { get; set; }
        public bool HasDiscount { get; set; }
    }
}