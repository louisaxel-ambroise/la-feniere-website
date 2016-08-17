using Gite.Cqrs;
using Gite.Cqrs.Commands;

namespace Gite.Messaging.Commands
{
    public class ReceivePayment : Command
    {
        public double Amount { get; set; }
    }
}