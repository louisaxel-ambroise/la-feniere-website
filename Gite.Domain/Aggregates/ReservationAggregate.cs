using System;
using Gite.Cqrs;
using Gite.Messaging.Events;
using Gite.Model.Model;

namespace Gite.Model.Aggregates
{
    public class ReservationAggregate : AggregateRoot
    {
        public DateTime BookedOn { get; set; }
        public bool IsCancelled { get; set; }
        public bool AdvancePaymentDeclared { get; set; }
        public bool AdvancePaymentReceived { get; set; }
        public Contact Contact { get; set; }

        public void Apply(ReservationCreated @event)
        {
            BookedOn = @event.OccuredOn;
            Contact = new Contact();
            // TODO: apply event
        }

        public void Apply(ReservationCancelled @event)
        {
            IsCancelled = true;
        }

        public void Apply(AdvancePaymentDeclared @event)
        {
            AdvancePaymentDeclared = true;
        }

        public void Apply(AdvancePaymentReceived @event)
        {
            AdvancePaymentReceived = true;
        }
    }
}