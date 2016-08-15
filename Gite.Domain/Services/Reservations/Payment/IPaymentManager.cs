using System;

namespace Gite.Model.Services.Reservations.Payment
{
    public interface IPaymentManager
    {
        void DeclareAdvancePaid(Guid id);
        void DeclarePaymentDone(Guid id);
        void DeclareAdvanceReceived(Guid id, double amount);
        void DeclarePaymentReceived(Guid id, double amount);
    }
}