using System;
using System.Collections.Generic;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Aggregates
{
    public interface IEventStore
    {
        IEnumerable<EventEnvelop> LoadForAggregate(Guid aggregateId);
        void Store(Guid aggregateId, Event @event);
    }
}