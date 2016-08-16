using System;
using Gite.Cqrs.Events;

namespace Gite.Messaging.Events
{
    public class AdvancePaymentReceived : Event
    {
        public Guid ReservationId { get; set; }
        public double Amount { get; set; }
    }
}