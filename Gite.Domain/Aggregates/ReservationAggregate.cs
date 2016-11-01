using System;
using Gite.Cqrs;
using Gite.Messaging.Events;
using Gite.Model.Model;

namespace Gite.Model.Aggregates
{
    public class ReservationAggregate : AggregateRoot
    {
        public DateTime BookedOn { get; set; }
        public DateTime AdvancePaymentLimit { get; set; }
        public bool IsLastMinute { get; set; }
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

        public ReservationAggregate(){ }

        public ReservationAggregate(Guid id, DateTime firstWeek, DateTime lastWeek, bool isLastMinute, Price price, Contact contact, People people)
        {
            Apply(new ReservationCreated
            {
                AggregateId = id,
                FirstWeek = firstWeek,
                LastWeek = lastWeek,
                IsLastMinute = isLastMinute,
                FinalPrice = price.Final,
                OriginalPrice = price.Original,
                Reduction = price.Reduction,
                Address = contact.Address,
                Mail = contact.Mail,
                Phone = contact.Phone,
                Name = contact.Name,
                AdultsCount = people.Adults,
                ChildrenCount = people.Children,
                BabiesCount = people.Babies,
                AnimalsCount = people.Animals,
                AnimalsType = people.AnimalsDescription
            });
        }

        public void DeclareAdvancePayment()
        {
            if (IsCancelled || BookedOn.AddDays(5) < DateTime.UtcNow) throw new Exception("Cannot perform any action when reservation is cancelled.");
            if(AdvancePaymentDeclared || AdvancePaymentReceived) throw new Exception("Advance Payment is already declared or received");

            Apply(new AdvancePaymentDeclared
            {
                AggregateId = Id
            });
        }

        public void ReceiveAdvancePayment(double amount)
        {
            if(IsCancelled) throw new Exception("Cannot perform any action when reservation is cancelled.");
            if (AdvancePaymentReceived) throw new Exception("Advance Payment is already received");

            Apply(new AdvancePaymentReceived
            {
                AggregateId = Id,
                Amount = amount
            });
        }

        public void DeclarePayment()
        {
            if (IsCancelled) throw new Exception("Cannot perform any action when reservation is cancelled.");
            if (PaymentDeclared || PaymentReceived) throw new Exception("Payment is already declared or received");

            Apply(new PaymentDeclared
            {
                AggregateId = Id                
            });
        }

        public void ReceivePayment(double amount)
        {
            if (IsCancelled) throw new Exception("Cannot perform any action when reservation is cancelled.");
            if (PaymentReceived) throw new Exception("Payment is already received");

            Apply(new PaymentReceived
            {
                AggregateId = Id,
                Amount = amount
            });
        }

        public void ExtendAdvancePaymentDelay(int days)
        {
            if(AdvancePaymentDeclared || AdvancePaymentReceived) throw new Exception("Cannot extend advance payment limit if advance is received.");

            Apply(new AdvancePaymentDelayExtended
            {
                AggregateId = Id,
                Days = days
            });
        }

        public void Cancel(string reason)
        {
            Apply(new ReservationCancelled
            {
                AggregateId = Id,
                Reason = reason
            });
        }

        public void Handle(ReservationCreated @event)
        {
            Id = @event.AggregateId;
            BookedOn = @event.OccuredOn;
            IsLastMinute = @event.IsLastMinute;
            AdvancePaymentLimit = @event.OccuredOn.AddDays(5);
            FirstWeek = @event.FirstWeek;
            LastWeek = @event.LastWeek;
            FinalPrice = @event.FinalPrice;
            DefaultPrice = @event.OriginalPrice;
            Reduction = @event.Reduction;

            Contact = new Contact { Name = @event.Name, Address = @event.Address, Mail = @event.Mail, Phone = @event.Phone };
            People = new People { Adults = @event.AdultsCount, Children = @event.ChildrenCount, Babies = @event.BabiesCount, Animals = @event.AnimalsCount, AnimalsDescription = @event.AnimalsType };
        }

        public void Handle(ReservationCancelled @event)
        {
            IsCancelled = true;
            CancellationReason = @event.Reason;
        }

        public void Handle(AdvancePaymentDelayExtended @event)
        {
            AdvancePaymentLimit = AdvancePaymentLimit.AddDays(@event.Days);
        }

        public void Handle(AdvancePaymentDeclared @event)
        {
            AdvancePaymentDeclared = true;
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            AdvancePaymentReceived = true;
            AdvancePaymentValue = @event.Amount;
        }

        public void Handle(PaymentDeclared @event)
        {
            PaymentDeclared = true;
        }

        public void Handle(PaymentReceived @event)
        {
            PaymentReceived = true;
            PaymentValue = @event.Amount;
        }
    }
}