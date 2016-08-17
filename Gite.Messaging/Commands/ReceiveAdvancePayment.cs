using Gite.Cqrs;
using Gite.Cqrs.Commands;

namespace Gite.Messaging.Commands
{
    public class ReceiveAdvancePayment : Command
    {
        public double Amount { get; set; } 
    }
}