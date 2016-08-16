using Gite.Cqrs.Events;

namespace Gite.Cqrs.Persistance
{
    public class EventStore : IEventStore
    {
        public void Store<T>(T @event) where T : IEvent
        {
        }
    }
}