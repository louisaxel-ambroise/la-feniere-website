using Gite.Cqrs;
using Gite.Cqrs.Commands;

namespace Gite.Messaging.Commands
{
    public class ExtendAdvancePaymentDelay : Command
    {
        public int Days { get; set; }
    }
}