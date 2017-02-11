using Gite.Domain.Model;
using System;

namespace Gite.Domain.Services.Reservations
{
    public interface IReservationManager
    {
        Guid Book(DateTime firstWeek, DateTime lastWeek, double expectedPrice, Contact contact, People people);
        void CancelReservation(Guid id, string reason);
        void DeclareAdvancePaymentDone(Guid id);
        void DeclarePaymentDone(Guid id);
        void DeclareAdvanceReceived(Guid id, double amount);
        void DeclarePaymentReceived(Guid id, double amount);
        void ExtendExpiration(Guid id, int days);
    }
}