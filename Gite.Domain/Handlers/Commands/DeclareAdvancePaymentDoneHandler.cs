using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class DeclareAdvancePaymentDoneHandler : ICommandHandler<DeclareAdvancePaymentDone>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;

        public DeclareAdvancePaymentDoneHandler(IAggregateManager<ReservationAggregate> aggregateManager)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");

            _aggregateManager = aggregateManager;
        }

        public void Handle(DeclareAdvancePaymentDone command)
        {
            var reservation = _aggregateManager.Load(command.AggregateId);
            reservation.DeclareAdvancePayment();

            _aggregateManager.Save(reservation);
        }
    }
}