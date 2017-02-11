using System;

namespace Gite.Domain.Model
{
    public class ReservationCalendar
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime FirstWeek { get; set; }
        public virtual DateTime LastWeek { get; set; }
        public virtual bool IsCancelled { get; set; }
        public virtual bool AdvancePaymentReceived { get; set; }
        public virtual bool PaymentReceived { get; set; }
        public virtual DateTime DisablesOn { get; set; }
    }
}
