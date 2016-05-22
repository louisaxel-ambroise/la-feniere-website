using Gite.Model.Interceptors;
using Gite.Model.Repositories;
using System;

namespace Gite.Model.Services.ReservationCanceller
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
        public void Cancel(Guid reservationId)
        {
            var reservation = _reservationRepository.Load(reservationId);

            if (reservation.CancelToken != null) throw new Exception(string.Format("Reservation {0} is already cancelled.", reservationId));

            reservation.CancelToken = reservation.Id;
        }
    }
}
