using System;
using Gite.Cqrs.Aggregates;

namespace Gite.Cqrs.Persistance
{
    public class AggregateLoader : IAggregateLoader
    {
        private readonly IEventLoader _eventRepository;

        public AggregateLoader(IEventLoader eventRepository)
        {
            if (eventRepository == null) throw new ArgumentNullException("eventRepository");

            _eventRepository = eventRepository;
        }

        public T Load<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            var aggregate = new T();
            var events = _eventRepository.LoadForAggregate(aggregateId);

            foreach (var @event in events)
            {
                aggregate.Apply(@event);
            }

            return aggregate;
        }
    }
}