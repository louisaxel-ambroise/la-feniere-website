using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Messaging.Events;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class ReceivePaymentHandler : CommandHandler<ReceivePayment>
    {
        private readonly IAggregateLoader _aggregateLoader;

        public ReceivePaymentHandler(IAggregateLoader aggregateLoader)
        {
            if (aggregateLoader == null) throw new ArgumentNullException("aggregateLoader");

            _aggregateLoader = aggregateLoader;
        }

        public override void Handle(ReceivePayment command)
        {
            var reservation = _aggregateLoader.Load<ReservationAggregate>(command.AggregateId);
            if (!CanHandle(reservation)) throw new Exception("Payment is already received.");

            RaiseEvent(new PaymentReceived
            {
                AggregateId = command.AggregateId,
                Amount = command.Amount
            });
        }

        private static bool CanHandle(ReservationAggregate reservation)
        {
            // Declare only once and max 5 days after booking.
            return !reservation.IsCancelled && reservation.AdvancePaymentReceived && !reservation.PaymentReceived;
        }
    }
}