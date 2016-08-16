using System;

namespace Gite.Model.Views
{
    public class BookedWeek
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ReservationId { get; set; }
        public virtual DateTime Week { get; set; }
        public virtual DateTime? DisablesOn { get; set; }
        public virtual Guid? CancellationToken { get; set; }
    }
}