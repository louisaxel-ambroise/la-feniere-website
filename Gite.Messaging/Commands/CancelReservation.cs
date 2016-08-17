using Gite.Cqrs;
using Gite.Cqrs.Commands;

namespace Gite.Messaging.Commands
{
    public class CancelReservation : Command
    {
        public string Reason { get; set; }
    }
}