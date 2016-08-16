using Gite.Cqrs;

namespace Gite.Messaging.Commands
{
    public class ExtendAdvancePaymentDelay : Command
    {
        public int Days { get; set; }
    }
}