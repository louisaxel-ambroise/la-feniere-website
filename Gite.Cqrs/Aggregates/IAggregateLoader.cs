using System;

namespace Gite.Cqrs.Aggregates
{
    public interface IAggregateLoader
    {
        T Load<T>(Guid aggregateId) where T : AggregateRoot, new();
    }
}