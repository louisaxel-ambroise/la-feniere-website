using Gite.Model.Model;

namespace Gite.Model.Services.ReservationPersister
{
    public interface IReservationPersister
    {
        void Persist(Reservation reservation);
    }
}