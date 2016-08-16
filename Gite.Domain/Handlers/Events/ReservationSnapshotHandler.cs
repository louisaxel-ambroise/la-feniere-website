using System;
using Gite.Cqrs.Events;
using Gite.Messaging.Events;
using Gite.Model.Model;
using Gite.Model.Repositories;

namespace Gite.Model.Handlers.Events
{
    public class ReservationSnapshotHandler : 
        IEventHandler<ReservationCreated>, IEventHandler<AdvancePaymentDeclared>, 
        IEventHandler<AdvancePaymentReceived>, IEventHandler<ReservationCancelled>
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationSnapshotHandler(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        public void Handle(ReservationCreated @event)
        {
            var reservation = new Reservation
            {
                Id = @event.AggregateId,
                BookedOn = @event.OccuredOn,
                FirstWeek = @event.FirstWeek,
                LastWeek = @event.LastWeek
            };

            _reservationRepository.Insert(reservation);
        }

        public void Handle(AdvancePaymentDeclared @event)
        {
            var reservation = _reservationRepository.Load(@event.AggregateId);

            reservation.AdvancePaymentDeclared = true;
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            var reservation = _reservationRepository.Load(@event.AggregateId);

            reservation.AdvancePaymentReceived = true;
            reservation.AdvancePaymentValue = @event.Amount;
        }

        public void Handle(ReservationCancelled @event)
        {
            var reservation = _reservationRepository.Load(@event.AggregateId);

            reservation.IsCancelled = true;
            reservation.CancellationReason = @event.Reason;
        }
    }
}