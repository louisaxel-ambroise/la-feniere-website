using Gite.Model.Interceptors;
using Gite.Model.Repositories;
using System;

namespace Gite.Model.Services.Reservations.Payment
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IReservationRepository _reservationRepository;

        public PaymentManager(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        [CommitTransaction]
        public void DeclareAdvancePaid(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.AdvancedDeclarationDate != null) throw new Exception("Advance is already declared");
            reservation.AdvancedDeclarationDate = DateTime.Now;
        }

        [CommitTransaction]
        public void DeclarePaymentDone(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.PaymentDeclarationDate != null) throw new Exception("Advance is already declared");
            reservation.PaymentDeclarationDate = DateTime.Now;
        }
    }
}
