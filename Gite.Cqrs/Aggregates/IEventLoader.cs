using System;
using System.Collections.Generic;

namespace Gite.Cqrs.Aggregates
{
    public interface IEventLoader
    {
        IEnumerable<EventEnvelop> LoadForAggregate(Guid aggregateId);
    }
}