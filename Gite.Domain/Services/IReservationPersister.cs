using Gite.Model.Model;

namespace Gite.Model.Services
{
    public interface IReservationPersister
    {
        void Persist(Reservation reservation);
    }
}