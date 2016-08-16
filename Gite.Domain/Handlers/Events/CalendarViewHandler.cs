using System;
using System.Linq;
using Gite.Cqrs.Events;
using Gite.Messaging.Events;
using Gite.Model.Readers;
using Gite.Model.Views;

namespace Gite.Model.Handlers.Events
{
    public class CalendarViewHandler : 
        IEventHandler<ReservationCreated>, IEventHandler<AdvancePaymentDeclared>, 
        IEventHandler<AdvancePaymentReceived>, IEventHandler<ReservationCancelled>,
        IEventHandler<AdvancePaymentDelayExtended>
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
                _bookedWeekRepository.Add(new BookedWeek { ReservationId = @event.AggregateId, Week = sat, DisablesOn = @event.OccuredOn.AddDays(5) });
            }
        }

        public void Handle(AdvancePaymentDeclared @event)
        {
            var bookedWeeks = _bookedWeekReader.QueryForReservation(@event.AggregateId).ToList();

            foreach (var bookedWeek in bookedWeeks)
            {
                bookedWeek.DisablesOn = @event.OccuredOn.AddDays(4); // 4 more days to validate advance payment reception.
            }
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            var bookedWeeks = _bookedWeekReader.QueryForReservation(@event.AggregateId).ToList();

            foreach (var bookedWeek in bookedWeeks)
            {
                bookedWeek.DisablesOn = null;
            }
        }

        public void Handle(ReservationCancelled @event)
        {
            var bookedWeeks = _bookedWeekReader.QueryForReservation(@event.AggregateId).ToList();

            foreach (var bookedWeek in bookedWeeks)
            {
                bookedWeek.Cancelled = true;
            }
        }

        public void Handle(AdvancePaymentDelayExtended @event)
        {
            var bookedWeeks = _bookedWeekReader.QueryForReservation(@event.AggregateId).ToList();

            foreach (var bookedWeek in bookedWeeks)
            {
                if (bookedWeek.DisablesOn != null)
                {
                    bookedWeek.DisablesOn = bookedWeek.DisablesOn.Value.AddDays(@event.Days);
                }
            }
        }
    }
}