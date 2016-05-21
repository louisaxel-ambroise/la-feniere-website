namespace Gite.Model.Services.DepositRefundProcessor
{
    public interface IRefundProcessor
    {
        void Process(string reservationId);
    }
}
