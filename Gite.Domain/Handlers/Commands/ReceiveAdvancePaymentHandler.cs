using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class ReceiveAdvancePaymentHandler : ICommandHandler<ReceiveAdvancePayment>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;

        public ReceiveAdvancePaymentHandler(IAggregateManager<ReservationAggregate> aggregateManager)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");

            _aggregateManager = aggregateManager;
        }

        public void Handle(ReceiveAdvancePayment command)
        {
            var reservation = _aggregateManager.Load(command.AggregateId);
            reservation.ReceiveAdvancePayment(command.Amount);

            _aggregateManager.Save(reservation);
        }
    }
}