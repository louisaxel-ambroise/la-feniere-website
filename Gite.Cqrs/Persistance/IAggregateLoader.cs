using System;

namespace Gite.Cqrs.Persistance
{
    public interface IAggregateLoader
    {
        T Load<T>(Guid aggregateId) where T : AggregateRoot, new();
    }
}