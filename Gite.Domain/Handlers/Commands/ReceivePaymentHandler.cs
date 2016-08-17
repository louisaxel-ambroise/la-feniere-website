using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class ReceivePaymentHandler : ICommandHandler<ReceivePayment>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;

        public ReceivePaymentHandler(IAggregateManager<ReservationAggregate> aggregateManager)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");

            _aggregateManager = aggregateManager;
        }

        public void Handle(ReceivePayment command)
        {
            var reservation = _aggregateManager.Load(command.AggregateId);
            reservation.ReceivePayment(command.Amount);

            _aggregateManager.Save(reservation);
        }
    }
}