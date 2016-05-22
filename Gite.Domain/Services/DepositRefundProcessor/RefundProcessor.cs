using Gite.Model.Interceptors;
using Gite.Model.Repositories;
using System;

namespace Gite.Model.Services.DepositRefundProcessor
{
    public class RefundProcessor : IRefundProcessor
    {
        private readonly IReservationRepository _reservationRepository;

        public RefundProcessor(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        [CommitTransaction]
        public void Process(Guid reservationId)
        {
            var reservation = _reservationRepository.Load(reservationId);

            if (reservation.CautionRefunded) throw new Exception(string.Format("Caution for reservation {0} already marked as refunded.", reservationId));
            if (reservation.IsCancelled()) throw new Exception(string.Format("Reservation {0} is cancelled.", reservationId));

            reservation.CautionRefunded = true;
        }
    }
}
