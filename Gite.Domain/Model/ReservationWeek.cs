using System;

namespace Gite.Model.Model
{
    public class ReservationWeek
    {
        public virtual Reservation Reservation { get; set; }
        public virtual DateTime StartsOn { get; set; }
        public virtual DateTime EndsOn { get; set; }
        public virtual Guid? CancellationToken { get; set; }
        public virtual double Price { get; internal set; }
        public virtual bool IsReserved { get; internal set; } // Not used in database.

        public virtual bool IsCancelled()
        {
            return CancellationToken != null;
        }
    }
}