using System;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;

namespace Gite.Model.Services.Reservations
{
    public class PaymentManager : IPaymentManager
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public PaymentManager(ICommandDispatcher commandDispatcher)
        {
            if (commandDispatcher == null) throw new ArgumentNullException("commandDispatcher");

            _commandDispatcher = commandDispatcher;
        }

        public void DeclareAdvancePaymentDone(Guid id)
        {
            _commandDispatcher.Dispatch(new DeclareAdvancePaymentDone
            {
                AggregateId = id
            });
        }

        public void DeclareAdvanceReceived(Guid id, double amount)
        {
            _commandDispatcher.Dispatch(new ReceiveAdvancePayment
            {
                AggregateId = id,
                Amount = amount
            });
        }

        public void DeclarePaymentDone(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeclarePaymentReceived(Guid id, double amount)
        {
            throw new NotImplementedException();
        }
    }
}