using System;
using Gite.Cqrs;

namespace Gite.Messaging.Commands
{
    public class CreateReservation : Command
    {
        public DateTime FirstWeek { get; set; }
        public DateTime LastWeek { get; set; }
    }
}