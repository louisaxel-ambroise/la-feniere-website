using System;

namespace Gite.WebSite.Models
{
    public class ReservationOverview
    {
        public Guid Id { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime StartsOn { get; set; }
        public DateTime LastWeek { get; set; }
        public DateTime EndsOn { get; set; }

        public bool PaymentReceived { get; set; }
        public bool PaymentDeclared { get; set; }

        public bool AdvancePaymentReceived { get; set; }
        public bool AdvancePaymentDeclared { get; set; }

        public bool Cancelled { get; set; }
        public DateTime? CancelledOn { get; set; }
        public string CancelReason { get; set; }

        public double FinalPrice { get; set; }
        public double OriginalPrice { get; set; }
        public double Reduction { get; set; }
        public double Caution { get; set; }

        public double AdvanceValue { get; set; }
    }
}