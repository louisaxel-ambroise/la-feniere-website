using Gite.Cqrs;

namespace Gite.Messaging.Commands
{
    public class ReceiveAdvancePayment : Command
    {
        public double Amount { get; set; } 
    }
}