using System;

namespace Gite.WebSite.Models.Admin
{
    public class ReservationModel
    {
        public Guid Id { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime FirstWeek { get; set; }
        public DateTime LastWeek { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public int Adults { get; internal set; }
        public int Children { get; internal set; }
        public int Babies { get; internal set; }
        public string AnimalsType { get; internal set; }
        public int Animals { get; internal set; }
        public bool AdvancedReceived { get; internal set; }
        public double? AdvancedValue { get; internal set; }
        public bool PaymentReceived { get; internal set; }
        public double? PaymentValue { get; internal set; }

        public double Reduction { get; set; }
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }
    }
}