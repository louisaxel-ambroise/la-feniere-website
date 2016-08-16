using System;
using Gite.Cqrs.Events;
using ReflectionMagic;

namespace Gite.Cqrs
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }

        public AggregateRoot() { }

        public void Apply(IEvent @event)
        {
            Apply(@event, true);
        }

        public void Apply(IEvent @event, bool isNew)
        {
            try
            {
                this.AsDynamic().Handle(@event);
            }
            catch
            {
                // TODO: log somewhere ?
            }
        }
    }
}