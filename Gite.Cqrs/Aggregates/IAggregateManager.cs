using System;

namespace Gite.Cqrs.Aggregates
{
    public interface IAggregateManager<T> where T : AggregateRoot, new()
    {
        T Load(Guid aggregateId);
        void Save(T aggregate);
    }
}