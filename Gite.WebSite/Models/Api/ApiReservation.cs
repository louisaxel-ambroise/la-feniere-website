using System;

namespace Gite.WebSite.Models.Api
{
    public class ApiReservation
    {
        public Guid Id { get; set; }
        public string CustomId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string StartingOn { get; set; }
        public string EndingOn { get; set; }
        public float Price { get; set; }
        public float Caution { get; set; }
        public bool PaymentDeclared { get; set; }
        public bool PaymentReceived { get; set; }
        public bool CautionRefunded { get; set; }
    }
}