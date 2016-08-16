using System;
using Gite.Cqrs;
using Gite.Messaging.Events;
using Gite.Model.Views;

namespace Gite.Model.Aggregates
{
    public class ReservationAggregate : AggregateRoot
    {
        public DateTime BookedOn { get; set; }
        public bool IsCancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool AdvancePaymentDeclared { get; set; }
        public bool AdvancePaymentReceived { get; set; }
        public double? AdvancePaymentValue { get; set; }
        public bool PaymentDeclared { get; set; }
        public bool PaymentReceived { get; set; }
        public double? PaymentValue { get; set; }
        public Contact Contact { get; set; }
        public People People { get; set; }
        public DateTime FirstWeek { get; set; }
        public DateTime LastWeek { get; set; }
        public double DefaultPrice { get; set; }
        public double FinalPrice { get; set; }
        public int Reduction { get; set; }

        public void Apply(ReservationCreated @event)
        {
            Id = @event.AggregateId;
            BookedOn = @event.OccuredOn;
            FirstWeek = @event.FirstWeek;
            LastWeek = @event.LastWeek;
            FinalPrice = @event.FinalPrice;
            DefaultPrice = @event.OriginalPrice;
            Reduction = @event.Reduction;

            Contact = new Contact{ Name = @event.Name, Address = @event.Address, Mail = @event.Mail, Phone = @event.Phone };
            People = new People{ Adults = @event.AdultsCount, Children = @event.ChildrenCount, Babies = @event.BabiesCount, Animals = @event.AnimalsCount, AnimalsDescription = @event.AnimalsType };
        }

        public void Apply(ReservationCancelled @event)
        {
            IsCancelled = true;
            CancellationReason = @event.Reason;
        }

        public void Apply(AdvancePaymentDeclared @event)
        {
            AdvancePaymentDeclared = true;
        }

        public void Apply(AdvancePaymentReceived @event)
        {
            AdvancePaymentReceived = true;
            AdvancePaymentValue = @event.Amount;
        }

        public void Apply(PaymentDeclared @event)
        {
            PaymentDeclared = true;
        }

        public void Apply(PaymentReceived @event)
        {
            PaymentReceived = true;
            PaymentValue = @event.Amount;
        }
    }
}