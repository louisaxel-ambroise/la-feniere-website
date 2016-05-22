using System;

namespace Gite.Model.Services.DepositRefundProcessor
{
    public interface IRefundProcessor
    {
        void Process(Guid reservationId);
    }
}
