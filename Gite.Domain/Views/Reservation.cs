using System;

namespace Gite.Model.Model
{
    public class Reservation
    {
        public virtual Guid Id { get; set; }
        public virtual double DefaultPrice { get; set; }
        public virtual double FinalPrice { get; set; }
        public virtual DateTime FirstWeek { get; set; }
        public virtual DateTime LastWeek { get; set; }
        public virtual DateTime BookedOn { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual People People { get; set; }
        public virtual double? PaymentValue { get; set; }
        public virtual bool IsCancelled { get; set; }
        public virtual string CancellationReason { get; set; }
        public virtual bool AdvancePaymentDeclared { get; set; }
        public virtual bool AdvancePaymentReceived { get; set; }
        public virtual double? AdvancePaymentValue { get; set; }
        public virtual bool PaymentDeclared { get; set; }
        public virtual bool PaymentReceived { get; set; }
    }
}
