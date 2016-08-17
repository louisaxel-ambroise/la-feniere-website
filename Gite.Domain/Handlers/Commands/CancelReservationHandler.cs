using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;

namespace Gite.Model.Handlers.Commands
{
    public class CancelReservationHandler : ICommandHandler<CancelReservation>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;

        public CancelReservationHandler(IAggregateManager<ReservationAggregate> aggregateManager)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");

            _aggregateManager = aggregateManager;
        }

        public void Handle(CancelReservation command)
        {
            var reservation = _aggregateManager.Load(command.AggregateId);
            reservation.Cancel(command.Reason);

            _aggregateManager.Save(reservation);
        }
    }
}