using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class DeclarePaymentDoneHandler : ICommandHandler<DeclarePaymentDone>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;

        public DeclarePaymentDoneHandler(IAggregateManager<ReservationAggregate> aggregateManager)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");

            _aggregateManager = aggregateManager;
        }

        public void Handle(DeclarePaymentDone command)
        {
            var reservation = _aggregateManager.Load(command.AggregateId);
            reservation.DeclarePayment();

            _aggregateManager.Save(reservation);
        }
    }
}