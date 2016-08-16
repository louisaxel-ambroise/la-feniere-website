using System;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Interceptors;
using Gite.Model.Model;

namespace Gite.Model.Services.Reservations
{
    public class Booker : IBooker
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Booker(ICommandDispatcher commandDispatcher)
        {
            if (commandDispatcher == null) throw new ArgumentNullException("commandDispatcher");

            _commandDispatcher = commandDispatcher;
        }

        [CommitTransaction]
        public Guid Book(DateTime firstWeek, DateTime lastWeek, ReservationDetails reservationDetails)
        {
            var reservationId = Guid.NewGuid();

            _commandDispatcher.Dispatch(new CreateReservation
            {
                AggregateId = reservationId,
                FirstWeek = firstWeek,
                LastWeek = lastWeek
            });

            return reservationId;
        }
    }
}