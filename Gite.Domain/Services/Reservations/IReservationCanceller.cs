using System;

namespace Gite.Model.Services.Reservations
{
    public interface IReservationCanceller
    {
        void CancelReservation(Guid id, string reason);
    }
}