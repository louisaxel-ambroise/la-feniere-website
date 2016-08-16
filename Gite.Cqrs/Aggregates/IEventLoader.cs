using System;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Aggregates
{
    public interface IEventLoader
    {
        IEvent[] LoadForAggregate(Guid aggregateId);
    }
}