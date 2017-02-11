namespace Gite.Domain.Model
{
    public class People
    {
        public virtual int Adults { get; set; }
        public virtual int Children { get; set; }
        public virtual int Babies { get; set; }
        public virtual int Animals { get; set; }
        public virtual string AnimalsDescription { get; set; }
    }
}