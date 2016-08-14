using Gite.Model.Model;
using System;

namespace Gite.Model.Services.Reservations.Actions
{
    public interface IReservationCanceller
    {
        void CancelReservation(Guid id, CancelReason? reason = null);
    }
}