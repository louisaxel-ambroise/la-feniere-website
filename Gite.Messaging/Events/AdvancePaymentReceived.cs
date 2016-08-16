using Gite.Cqrs.Events;

namespace Gite.Messaging.Events
{
    public class AdvancePaymentReceived : Event
    {
        public double Amount { get; set; }
    }
}