using System;
using Gite.Cqrs.Events;

namespace Gite.Messaging.Events
{
    public class ReservationCreated : Event
    {
        public DateTime FirstWeek { get; set; }
        public DateTime LastWeek { get; set; }
    }
}