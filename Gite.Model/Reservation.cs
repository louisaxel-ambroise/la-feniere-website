using System;

namespace Gite.Model
{
    public class Reservation
    {
        public virtual string Id { get; set; }
        public virtual DateTime StartingOn { get; set; }
        public virtual DateTime EndingOn { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual bool Validated { get; set; }
        public virtual bool Confirmed { get; set; }
        public virtual int Price { get; set; }
    }
}
