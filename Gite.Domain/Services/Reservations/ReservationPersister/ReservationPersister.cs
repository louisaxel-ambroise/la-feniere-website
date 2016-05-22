using System;
using Gite.Model.Interceptors;
using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services.MailSender;

namespace Gite.Model.Services.ReservationPersister
{
    public class ReservationPersister : IReservationPersister
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationConfirmationMailSender _reservationMailSender;

        public ReservationPersister(IReservationRepository reservationRepository, IReservationConfirmationMailSender reservationMailSender)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (reservationMailSender == null) throw new ArgumentNullException("reservationMailSender");

            _reservationRepository = reservationRepository;
            _reservationMailSender = reservationMailSender;
        }

        [CommitTransaction]
        public virtual void Persist(Reservation reservation)
        {
            _reservationRepository.Insert(reservation);

            if (_reservationMailSender.ConfirmReservation(reservation))
            {
                // TODO: update reservation to confirm mail has been sent.
            }
        }
    }
}