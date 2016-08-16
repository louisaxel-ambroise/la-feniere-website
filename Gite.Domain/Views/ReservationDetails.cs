namespace Gite.Model.Model
{
    public class ReservationDetails
    {
        public Contact Contact { get; set; }
        public People People { get; set; }
    }

    public class People
    {
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public int Animals { get; set; }
        public string AnimalsDescription { get; set; }
    }

    public class Contact
    {
        public string Address { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}