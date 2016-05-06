namespace Gite.Model.Business
{
    public class PriceResponse
    {
        public bool Match { get; set; }
        public int Amount { get; set; }
        public bool HasDiscount { get; set; }
    }
}