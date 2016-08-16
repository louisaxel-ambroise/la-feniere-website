using Gite.Cqrs;

namespace Gite.Messaging.Commands
{
    public class CancelReservation : Command
    {
        public string Reason { get; set; }
    }
}