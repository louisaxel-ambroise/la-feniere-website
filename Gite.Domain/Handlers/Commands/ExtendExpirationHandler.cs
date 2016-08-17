using System;
using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;
using Gite.Model.Readers;

namespace Gite.Model.Handlers.Commands
{
    public class ExtendAdvancePaymentDelayHandler : ICommandHandler<ExtendAdvancePaymentDelay>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;
        private readonly IBookedWeekReader _bookedWeekReader;

        public ExtendAdvancePaymentDelayHandler(IAggregateManager<ReservationAggregate> aggregateManager, IBookedWeekReader bookedWeekReader)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");
            if (bookedWeekReader == null) throw new ArgumentNullException("bookedWeekReader");

            _aggregateManager = aggregateManager;
            _bookedWeekReader = bookedWeekReader;
        }

        public void Handle(ExtendAdvancePaymentDelay command)
        {
            var reservation = _aggregateManager.Load(command.AggregateId);

            if (_bookedWeekReader.QueryValids().Any(x => x.ReservationId != reservation.Id && x.Week >= reservation.FirstWeek && x.Week <= reservation.LastWeek))
                throw new Exception("Another reservation has been booked for the same dates.");

            reservation.ExtendAdvancePaymentDelay(command.Days);

            _aggregateManager.Save(reservation);
        }
    }
}