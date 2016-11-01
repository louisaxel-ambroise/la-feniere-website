using System;
using Gite.Cqrs.Commands;

namespace Gite.Messaging.Commands
{
    public class CreateReservation : Command
    {
        public DateTime FirstWeek { get; set; }
        public DateTime LastWeek { get; set; }
        public bool IsLastMinute { get; set; }
        public double FinalPrice { get; set; }
        public double OriginalPrice { get; set; }
        public int Reduction{ get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int BabiesCount { get; set; }
        public int AnimalsCount { get; set; }
        public string AnimalsType { get; set; }
    }
}