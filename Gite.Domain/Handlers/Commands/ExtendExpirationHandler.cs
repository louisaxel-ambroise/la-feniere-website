using System;
using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Messaging.Events;
using Gite.Model.Aggregates;
using Gite.Model.Readers;

namespace Gite.Model.Handlers.Commands
{
    public class ExtendAdvancePaymentDelayHandler : CommandHandler<ExtendAdvancePaymentDelay>
    {
        private readonly IAggregateLoader _aggregateLoader;
        private readonly IBookedWeekReader _bookedWeekReader;

        public ExtendAdvancePaymentDelayHandler(IAggregateLoader aggregateLoader, IBookedWeekReader bookedWeekReader)
        {
            if (aggregateLoader == null) throw new ArgumentNullException("aggregateLoader");
            if (bookedWeekReader == null) throw new ArgumentNullException("bookedWeekReader");

            _aggregateLoader = aggregateLoader;
            _bookedWeekReader = bookedWeekReader;
        }

        public override void Handle(ExtendAdvancePaymentDelay command)
        {
            var reservation = _aggregateLoader.Load<ReservationAggregate>(command.AggregateId);

            if (_bookedWeekReader.QueryValids().Any(x => x.ReservationId != reservation.Id && x.Week >= reservation.FirstWeek && x.Week <= reservation.LastWeek)) {
                throw new Exception("Another reservation has been booked for the same dates.");
            }

            RaiseEvent(new AdvancePaymentDelayExtended
            {
                AggregateId = command.AggregateId,
                Days = command.Days
            });
        }
    }
}