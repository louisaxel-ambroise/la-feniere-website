using System;
using Gite.Model.Interceptors;
using Gite.Model.Model;
using Gite.Model.Repositories;

namespace Gite.Model.Services.ReservationPersister
{
    public class ReservationPersister : IReservationPersister
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationPersister(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");

            _reservationRepository = reservationRepository;
        }

        [CommitTransaction]
        public virtual void Persist(Reservation reservation)
        {
            _reservationRepository.Insert(reservation);
        }
    }
}