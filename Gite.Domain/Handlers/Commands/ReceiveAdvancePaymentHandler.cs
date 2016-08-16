using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Messaging.Events;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class ReceiveAdvancePaymentHandler : CommandHandler<ReceiveAdvancePayment>
    {
        private readonly IAggregateLoader _aggregateLoader;

        public ReceiveAdvancePaymentHandler(IAggregateLoader aggregateLoader)
        {
            if (aggregateLoader == null) throw new ArgumentNullException("aggregateLoader");

            _aggregateLoader = aggregateLoader;
        }

        public override void Handle(ReceiveAdvancePayment command)
        {
            var reservation = _aggregateLoader.Load<ReservationAggregate>(command.AggregateId);
            if (!CanHandle(reservation)) throw new Exception("Advance payment is already received.");

            RaiseEvent(new AdvancePaymentReceived
            {
                OccuredOn = DateTime.Now,
                AggregateId = command.AggregateId
            });
        }

        private static bool CanHandle(ReservationAggregate reservation)
        {
            return reservation != null && !reservation.IsCancelled && !reservation.AdvancePaymentReceived;
        }
    }
}