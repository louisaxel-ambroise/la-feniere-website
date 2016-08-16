using Gite.Cqrs.Events;

namespace Gite.Cqrs.Persistance
{
    public interface IEventStore
    {
        void Store<T>(T @event) where T : IEvent;
    }
}