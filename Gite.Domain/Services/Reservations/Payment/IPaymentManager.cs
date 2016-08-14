using System;

namespace Gite.Model.Services.Reservations.Payment
{
    public interface IPaymentManager
    {
        void DeclareAdvancePaid(Guid id);
        void DeclarePaymentDone(Guid id);
    }
}