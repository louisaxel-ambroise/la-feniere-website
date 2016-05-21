using Gite.Model.Interceptors;
using Gite.Model.Repositories;
using System;

namespace Gite.Model.Services.PaymentProcessor
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IReservationRepository _reservationRepository;

        public PaymentProcessor(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        [CommitTransaction]
        public void PaymentReceived(string reservationId)
        {
            var reservation = _reservationRepository.Load(reservationId);

            if (reservation.PaymentReceived) throw new Exception(string.Format("Payment for reservation {0} already marked as received.", reservationId));

            reservation.PaymentReceived = true;
        }
    }
}
