using Gite.Cqrs.Events;

namespace Gite.Messaging.Events
{
    public class AdvancePaymentDelayExtended : Event
    {
        public int Days { get; set; }
    }
}