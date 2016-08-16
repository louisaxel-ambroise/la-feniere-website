using System;

namespace Gite.Model.Services.Reservations
{
    public interface IPaymentManager
    {
        void DeclareAdvancePaymentDone(Guid id);
        void DeclarePaymentDone(Guid id);
        void DeclareAdvanceReceived(Guid id, double amount);
        void DeclarePaymentReceived(Guid id, double amount);
        void ExtendExpiration(Guid id, int days);
    }
}