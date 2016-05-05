using System;

namespace Gite.Model
{
    public class Reservation
    {
        public virtual string Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual bool Validated { get; set; }
        public virtual bool Confirmed { get; set; }
    }
}
