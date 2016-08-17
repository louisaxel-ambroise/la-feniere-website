using System;
using Gite.Cqrs.Events;
using Gite.Messaging.Events;
using Gite.Model.Model;
using Gite.Model.Readers;

namespace Gite.Model.Handlers.Events
{
    public class ReservationSnapshotHandler : 
        IEventHandler<ReservationCreated>, IEventHandler<AdvancePaymentDeclared>, 
        IEventHandler<PaymentDeclared>, IEventHandler<PaymentReceived>,
        IEventHandler<AdvancePaymentReceived>, IEventHandler<ReservationCancelled>
    {
        private readonly IReservationReader _reservationReader;

        public ReservationSnapshotHandler(IReservationReader reservationReader)
        {
            if (reservationReader == null) throw new ArgumentNullException("reservationReader");

            _reservationReader = reservationReader;
        }

        public void Handle(ReservationCreated @event)
        {
            var reservation = new Reservation
            {
                Id = @event.AggregateId,
                BookedOn = @event.OccuredOn,
                FirstWeek = @event.FirstWeek,
                LastWeek = @event.LastWeek,
                FinalPrice = @event.FinalPrice,
                Mail = @event.Mail,
                Name = @event.Name,
                Phone = @event.Phone,
                PeopleNumber = @event.AdultsCount + @event.ChildrenCount + @event.BabiesCount
            };

            _reservationReader.Insert(reservation);
        }

        public void Handle(AdvancePaymentDeclared @event)
        {
            var reservation = _reservationReader.Load(@event.AggregateId);

            reservation.AdvancePaymentDeclared = true;
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            var reservation = _reservationReader.Load(@event.AggregateId);

            reservation.AdvancePaymentReceived = true;
        }

        public void Handle(PaymentDeclared @event)
        {
            var reservation = _reservationReader.Load(@event.AggregateId);

            reservation.PaymentDeclared = true;
        }

        public void Handle(PaymentReceived @event)
        {
            var reservation = _reservationReader.Load(@event.AggregateId);

            reservation.PaymentReceived = true;
        }

        public void Handle(ReservationCancelled @event)
        {
            var reservation = _reservationReader.Load(@event.AggregateId);

            reservation.IsCancelled = true;
            reservation.CancellationReason = @event.Reason;
        }
    }
}