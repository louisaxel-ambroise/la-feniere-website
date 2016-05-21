namespace Gite.Model.Services.PaymentProcessor
{
    public interface IPaymentProcessor
    {
        void PaymentReceived(string reservationId);
    }
}
