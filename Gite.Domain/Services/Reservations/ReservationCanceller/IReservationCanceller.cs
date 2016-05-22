using System;

namespace Gite.Model.Services.ReservationCanceller
{
    public interface IReservationCanceller
    {
        void Cancel(Guid reservationId);
    }
}
