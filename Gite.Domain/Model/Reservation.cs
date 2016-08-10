using System;
using System.Collections.Generic;
using System.Linq;

namespace Gite.Model.Model
{
    public class Reservation
    {
        public virtual Guid Id { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual IList<ReservationWeek> Weeks { get; set; }
        public virtual DateTime BookedOn { get; set; }
        public virtual DateTime? AdvancedReceptionDate { get; set; }
        public virtual DateTime? CancelledOn { get; set; }
        public virtual Guid? CancellationToken { get; set; }

        public virtual bool IsValid()
        {
            return !CancellationToken.HasValue && (AdvancedReceptionDate.HasValue || BookedOn.Date < DateTime.Now.AddDays(5));
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

        public virtual bool ContainsDate(DateTime firstDayOfWeek)
        {
            return StartsOn() <= firstDayOfWeek && EndsOn() > firstDayOfWeek;
        }
    }
}
