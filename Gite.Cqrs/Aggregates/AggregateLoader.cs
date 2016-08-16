using System;
using System.Linq;
using Gite.Cqrs.Extensions;

namespace Gite.Cqrs.Aggregates
{
    public class AggregateLoader : IAggregateLoader
    {
        private readonly IEventLoader _eventLoader;
        private readonly Type[] _eventTypes;

        public AggregateLoader(IEventLoader eventLoader, Type[] eventTypes)
        {
            if (eventLoader == null) throw new ArgumentNullException("eventLoader");
            if (eventTypes == null) throw new ArgumentNullException("eventTypes");

            _eventLoader = eventLoader;
            _eventTypes = eventTypes;
        }

        public T Load<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            var events = _eventLoader.LoadForAggregate(aggregateId).Select(x => x.CastAsEvent(_eventTypes)).ToArray();

            var aggregate = new T
            {
                Events = events
            };
            
            foreach (var @event in events)
            {
                aggregate.Apply(@event);
            }

            return aggregate;
        }
    }
}