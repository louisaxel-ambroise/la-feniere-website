using System;

namespace Gite.Domain.Model
{
    public class Week
    {
        public Guid? ReservationId { get; set; }
        public DateTime Start { get; set; }
        public bool IsReserved { get; set; }
        public bool IsValidated { get; set; }
        public double Price { get; set; }
    }
}