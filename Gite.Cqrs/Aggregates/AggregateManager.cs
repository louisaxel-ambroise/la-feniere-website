using System;
using System.Linq;
using Gite.Cqrs.Events;
using Gite.Cqrs.Extensions;

namespace Gite.Cqrs.Aggregates
{
    public class AggregateManager<T> : IAggregateManager<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly Type[] _eventTypes;

        public AggregateManager(IEventStore eventStore, IEventDispatcher eventDispatcher, Type[] eventTypes)
        {
            if (eventStore == null) throw new ArgumentNullException("eventStore");
            if (eventDispatcher == null) throw new ArgumentNullException("eventDispatcher");
            if (eventTypes == null) throw new ArgumentNullException("eventTypes");

            _eventStore = eventStore;
            _eventDispatcher = eventDispatcher;
            _eventTypes = eventTypes;
        }

        public T Load(Guid aggregateId)
        {
            var events = _eventStore.LoadForAggregate(aggregateId).Select(x => x.CastAsEvent(_eventTypes)).ToArray();
            var aggregate = new T { Events = events };

            foreach (var @event in events) aggregate.Apply(@event, false);

            return aggregate;
        }

        public void Save(T aggregate)
        {
            foreach (var pendingEvent in aggregate.PendingEvents())
            {
                _eventStore.Store(aggregate.Id, pendingEvent);
                _eventDispatcher.Dispatch(pendingEvent);

                aggregate.Commit(pendingEvent);
            }
        }
    }
}