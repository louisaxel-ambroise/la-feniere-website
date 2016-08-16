using System;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Interceptors;

namespace Gite.Model.Services.Reservations
{
    public class ReservationCanceller : IReservationCanceller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ReservationCanceller(ICommandDispatcher commandDispatcher)
        {
            if (commandDispatcher == null) throw new ArgumentNullException("commandDispatcher");

            _commandDispatcher = commandDispatcher;
        }

        [CommitTransaction]
        public void CancelReservation(Guid id, string reason)
        {
            _commandDispatcher.Dispatch(new CancelReservation
            {
                AggregateId = id,
                Reason = reason
            });
        }
    }
}