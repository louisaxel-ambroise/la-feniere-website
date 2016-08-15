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
        public void DeclareAdvanceReceived(Guid id, double amount)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.AdvancedReceptionDate != null) throw new Exception("Advance is already received");
            reservation.AdvancedReceptionDate = DateTime.Now;
            reservation.AdvancedValue = amount;
        }

        [CommitTransaction]
        public void DeclarePaymentDone(Guid id)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.PaymentDeclarationDate != null) throw new Exception("Advance is already declared");
            reservation.PaymentDeclarationDate = DateTime.Now;
        }

        [CommitTransaction]
        public void DeclarePaymentReceived(Guid id, double amount)
        {
            var reservation = _reservationRepository.Load(id);

            if (reservation.PaymentReceptionDate != null) throw new Exception("Advance is already declared");
            reservation.PaymentReceptionDate = DateTime.Now;
            reservation.PaymentValue = amount;
        }
    }
}
