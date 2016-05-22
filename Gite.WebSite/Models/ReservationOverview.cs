using System;

namespace Gite.WebSite.Models
{
    public class ReservationOverview
    {
        public Guid Id { get; set; }
        public string CustomId { get; set; }
        public DateTime StartingOn { get; set; }
        public DateTime EndingOn { get; set; }
        public float Caution { get; set; }
        public float Price { get; set; }
        public bool Cancelled { get; set; }
        public bool PaymentDeclared { get; set; }
        public bool PaymentReceived { get; set; }
        public bool CautionRefunded { get; set; }
    }
}