using Gite.Model.Interceptors;
using Gite.Model.Model;
using Gite.Model.Repositories;
using System;

namespace Gite.Model.Services.Reservations.Actions
{
    public class ReservationCanceller : IReservationCanceller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationCanceller(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        [CommitTransaction]
        public void CancelReservation(Guid id, CancelReason? reason = null)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.CancellationToken.HasValue) throw new Exception("Reservation is already cancelled.");

            reservation.CancellationReason = reason;
            reservation.CancellationToken = Guid.NewGuid();
            reservation.CancelledOn = DateTime.Now;
        }
    }
}
