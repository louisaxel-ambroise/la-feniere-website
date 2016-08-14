namespace Gite.Model.Model
{
    public class People
    {
        public virtual Reservation Reservation { get; set; }
        public virtual int Adults { get; set; }
        public virtual int Children { get; set; }
        public virtual int Babies { get; set; }
        public virtual int Animals { get; set; }
        public virtual string AnimalsDescription { get; set; }
    }
}
