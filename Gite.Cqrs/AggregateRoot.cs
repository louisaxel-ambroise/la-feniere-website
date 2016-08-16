using System;
using System.Collections.Generic;
using Gite.Cqrs.Events;
using ReflectionMagic;

namespace Gite.Cqrs
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }
        public IEnumerable<Event> Events { get; set; }

        public AggregateRoot()
        {
            Events = new List<Event>();
        }

        public void Apply(Event @event)
        {
            Apply(@event, true);
        }

        public void Apply(Event @event, bool isNew)
        {
            try
            {
                this.AsDynamic().Apply(@event);
            }
            catch
            {
                // TODO: log somewhere ?
            }
        }
    }
}