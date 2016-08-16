using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Messaging.Events;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class DeclareAdvancePaymentDoneHandler : CommandHandler<DeclareAdvancePaymentDone>
    {
        private readonly IAggregateLoader _aggregateLoader;

        public DeclareAdvancePaymentDoneHandler(IAggregateLoader aggregateLoader)
        {
            if (aggregateLoader == null) throw new ArgumentNullException("aggregateLoader");

            _aggregateLoader = aggregateLoader;
        }

        public override void Handle(DeclareAdvancePaymentDone command)
        {
            var reservation = _aggregateLoader.Load<ReservationAggregate>(command.AggregateId);
            if(!CanHandle(reservation)) throw new Exception("Advance payment is already received.");

            RaiseEvent(new AdvancePaymentDeclared
            {
                OccuredOn = DateTime.Now,
                AggregateId = command.AggregateId
            });
        }

        private static bool CanHandle(ReservationAggregate reservation)
        {
            // Declare only once and max 5 days after booking.
            return !reservation.IsCancelled && reservation.BookedOn.AddDays(5) >= DateTime.Now && !reservation.AdvancePaymentDeclared && !reservation.AdvancePaymentReceived;
        }
    }
}