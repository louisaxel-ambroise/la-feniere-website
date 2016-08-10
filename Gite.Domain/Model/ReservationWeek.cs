using System;

namespace Gite.Model.Model
{
    public class ReservationWeek
    {
        public virtual Reservation Reservation { get; set; }
        public virtual DateTime StartsOn { get; set; }
        public virtual DateTime EndsOn { get; set; }
        public virtual Guid? CancellationToken { get; set; }
    }
}