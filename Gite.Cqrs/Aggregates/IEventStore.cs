using Gite.Cqrs.Events;

namespace Gite.Cqrs.Aggregates
{
    public interface IEventStore
    {
        void Store<T>(T @event) where T : Event;
    }
}