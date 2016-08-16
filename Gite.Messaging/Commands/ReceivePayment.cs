using Gite.Cqrs;

namespace Gite.Messaging.Commands
{
    public class ReceivePayment : Command
    {
        public double Amount { get; set; }
    }
}