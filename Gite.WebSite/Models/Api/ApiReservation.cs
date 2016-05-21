namespace Gite.WebSite.Models.Api
{
    public class ApiReservation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string StartingOn { get; set; }
        public string EndingOn { get; set; }
        public int Price { get; set; }
        public bool PaymentReceived { get; set; }
        public bool CautionRefunded { get; set; }
    }
}