using System;
using Gite.Model.Interceptors;
using Gite.Model.Model;
using Gite.Model.Repositories;

namespace Gite.Model.Services.Reservations
{
    public class Booker : IBooker
    {
        private readonly IReservationRepository _repository;

        public Booker(IReservationRepository repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        [CommitTransaction]
        public Guid Book(DateTime @from, DateTime to, Contact contact)
        {
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                Contact = contact,
                BookedOn = DateTime.Now
            };



            _repository.Insert(reservation);

            return reservation.Id;
        }
    }
}