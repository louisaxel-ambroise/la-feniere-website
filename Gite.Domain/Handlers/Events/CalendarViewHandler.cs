using System;
using System.Linq;
using Gite.Cqrs.Events;
using Gite.Messaging.Events;
using Gite.Model.Model;
using Gite.Model.Repositories;

namespace Gite.Model.Handlers.Events
{
    public class CalendarViewHandler : IEventHandler<ReservationCreated>, IEventHandler<AdvancePaymentReceived>, IEventHandler<ReservationCancelled>
    {
        private readonly IBookedWeekRepository _bookedWeekRepository;
        private readonly IBookedWeekReader _bookedWeekReader;

        public CalendarViewHandler(IBookedWeekRepository bookedWeekRepository, IBookedWeekReader bookedWeekReader)
        {
            if (bookedWeekRepository == null) throw new ArgumentNullException("bookedWeekRepository");
            if (bookedWeekReader == null) throw new ArgumentNullException("bookedWeekReader");

            _bookedWeekRepository = bookedWeekRepository;
            _bookedWeekReader = bookedWeekReader;
        }

        public void Handle(ReservationCreated @event)
        {
            for (var sat = @event.FirstWeek; sat <= @event.LastWeek; sat = sat.AddDays(7))
            {
                _bookedWeekRepository.Add(new BookedWeek { ReservationId = @event.AggregateId, Week = sat });
            }
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            var bookedWeeks = _bookedWeekReader.QueryForReservation(@event.ReservationId).ToList();

            foreach (var bookedWeek in bookedWeeks)
            {
                bookedWeek.DisablesOn = null;
            }
        }

        public void Handle(ReservationCancelled @event)
        {
            var bookedWeeks = _bookedWeekReader.QueryForReservation(@event.ReservationId).ToList();

            foreach (var bookedWeek in bookedWeeks)
            {
                bookedWeek.Cancelled = true;
            }
        }
    }
}