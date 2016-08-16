using System;
using Gite.Cqrs.Events;

namespace Gite.Messaging.Events
{
    public class ReservationCancelled : Event
    {
        public Guid ReservationId { get; set; }
        public string Reason { get; set; }
    }
}