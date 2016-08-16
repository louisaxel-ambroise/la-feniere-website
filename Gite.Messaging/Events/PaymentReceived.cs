using Gite.Cqrs.Events;

namespace Gite.Messaging.Events
{
    public class PaymentReceived : Event
    {
        public double Amount { get; set; }
    }
}