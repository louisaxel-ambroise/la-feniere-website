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
        public virtual DateTime? AdvancedDeclarationDate { get; set; }
        public virtual DateTime? AdvancedReceptionDate { get; set; }
        public virtual double? AdvancedValue { get; set; }
        public virtual DateTime? PaymentDeclarationDate { get; set; }
        public virtual DateTime? PaymentReceptionDate { get; set; }
        public virtual double? PaymentValue { get; set; }
        public virtual DateTime? CancelledOn { get; set; }
        public virtual CancelReason? CancellationReason { get; set; }
        public virtual Guid? CancellationToken { get; set; }

        public virtual bool IsValid()
        {
            return CancellationToken == null && (AdvancedReceptionDate != null || BookedOn < DateTime.Now.AddDays(5));
        }

        public virtual double ComputeDiscount()
        {
            var weeksNumber = (LastWeek - FirstWeek).Days / 7;

            if (weeksNumber == 2) return 3;
            if (weeksNumber > 2) return 4;

            return 0;
        }

        public virtual bool ContainsDate(DateTime date)
        {
            return FirstWeek >= date && LastWeek.AddDays(7) < date;
        }
    }
}
