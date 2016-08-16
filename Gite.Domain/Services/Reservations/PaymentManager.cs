using System;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Interceptors;

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

        [CommitTransaction]
        public void DeclareAdvancePaymentDone(Guid id)
        {
            _commandDispatcher.Dispatch(new DeclareAdvancePaymentDone
            {
                AggregateId = id
            });
        }

        [CommitTransaction]
        public void DeclareAdvanceReceived(Guid id, double amount)
        {
            _commandDispatcher.Dispatch(new ReceiveAdvancePayment
            {
                AggregateId = id,
                Amount = amount
            });
        }

        [CommitTransaction]
        public void DeclarePaymentDone(Guid id)
        {
            _commandDispatcher.Dispatch(new DeclarePaymentDone
            {
                AggregateId = id
            });
        }

        [CommitTransaction]
        public void DeclarePaymentReceived(Guid id, double amount)
        {
            _commandDispatcher.Dispatch(new ReceivePayment
            {
                AggregateId = id,
                Amount = amount
            });
        }

        [CommitTransaction]
        public void ExtendExpiration(Guid id, int days)
        {
            _commandDispatcher.Dispatch(new ExtendAdvancePaymentDelay
            {
                AggregateId = id,
                Days = days
            });
        }
    }
}