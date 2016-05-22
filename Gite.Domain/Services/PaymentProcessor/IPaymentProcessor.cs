using System;

namespace Gite.Model.Services.PaymentProcessor
{
    public interface IPaymentProcessor
    {
        void PaymentReceived(Guid reservationId);
    }
}
