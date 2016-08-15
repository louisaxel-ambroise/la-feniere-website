namespace Gite.Model.Model
{
    public class Contact
    {
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Phone { get; set; }
    }

    public class ReservationDetails
    {
        public Contact Contact { get; set; }
        public People People { get; set; }
    }
}