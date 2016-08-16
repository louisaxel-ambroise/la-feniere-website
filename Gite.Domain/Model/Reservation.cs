using System;

namespace Gite.Model.Views
{
    public class Reservation
    {
        public virtual Guid Id { get; set; }
        public virtual double FinalPrice { get; set; }
        public virtual DateTime FirstWeek { get; set; }
        public virtual DateTime LastWeek { get; set; }
        public virtual DateTime BookedOn { get; set; }
        public virtual string Name { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Phone { get; set; }
        public virtual int PeopleNumber { get; set; }
        public virtual bool IsCancelled { get; set; }
        public virtual string CancellationReason { get; set; }
        public virtual bool AdvancePaymentDeclared { get; set; }
        public virtual bool AdvancePaymentReceived { get; set; }
        public virtual bool PaymentDeclared { get; set; }
        public virtual bool PaymentReceived { get; set; }
    }
}
