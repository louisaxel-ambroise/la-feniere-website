using System;

namespace Gite.Model.Model
{
    public class BookedWeek
    {
        public virtual Guid ReservationId { get; set; }
        public virtual DateTime Week { get; set; }
        public virtual DateTime? DisablesOn { get; set; }
        public virtual bool Cancelled { get; set; }

        public virtual bool IsValid()
        {
            return !Cancelled && (DisablesOn == null || DisablesOn > DateTime.Now);
        }
    }
}