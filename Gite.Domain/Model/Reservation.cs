using System;
using System.Collections.Generic;
using System.Linq;

namespace Gite.Model.Model
{
    public class Reservation
    {
        public virtual Guid Id { get; set; }
        public virtual double PriceWithDiscount { get; set; }
        public virtual double Advanced { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual IList<ReservationWeek> Weeks { get; set; }
        public virtual DateTime BookedOn { get; set; }
        public virtual DateTime? AdvancedReceptionDate { get; set; }
        public virtual DateTime? CancelledOn { get; set; }
        public virtual Guid? CancellationToken { get; set; }

        public Reservation()
        {
            Weeks = new List<ReservationWeek>();
        }

        public virtual bool IsValid()
        {
            return !CancellationToken.HasValue && (AdvancedReceptionDate.HasValue || BookedOn.Date < DateTime.Now.AddDays(5));
        }

        public virtual double Reduction()
        {
            if (Weeks.Count == 2) return 3;
            if (Weeks.Count > 2) return 4;

            return 0;
        }

        public virtual double Price()
        {
            var totalWeeks = Weeks.Sum(x => x.Price);
            var reduction = totalWeeks * Reduction() / 100;

            return Math.Ceiling(totalWeeks - reduction);
        }

        public virtual DateTime StartsOn()
        {
            return Weeks.OrderBy(x => x.StartsOn).First().StartsOn;
        }

        public virtual DateTime EndsOn()
        {
            return Weeks.OrderBy(x => x.EndsOn).Last().EndsOn;
        }

        public virtual void Cancel()
        {
            CancellationToken = Guid.NewGuid();
            CancelledOn = DateTime.Now;

            foreach (var week in Weeks)
            {
                week.CancellationToken = CancellationToken;
            }
        }

        public virtual bool ContainsDate(DateTime date)
        {
            return StartsOn() >= date && EndsOn().AddDays(7) <= date;
        }
    }
}
