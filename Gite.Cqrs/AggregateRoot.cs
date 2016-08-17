using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Events;
using ReflectionMagic;

namespace Gite.Cqrs
{
    public abstract class AggregateRoot
    {
        private readonly IList<Event> _pending;
        public Guid Id { get; protected set; }
        public IEnumerable<Event> Events { get; set; }

        protected AggregateRoot()
        {
            Events = new List<Event>();
            _pending = new List<Event>();
        }

        public Event[] PendingEvents()
        {
            return _pending.ToArray();
        }

        public void Commit(Event @event)
        {
            _pending.Remove(@event);
        }

        public void Apply(Event @event)
        {
            Apply(@event, true);
        }

        public void Apply(Event @event, bool isNew)
        {
            try
            {
                this.AsDynamic().Handle(@event);
            }
            catch
            {
                // TODO: log somewhere ?
            }

            if (isNew)
            {
                _pending.Add(@event);
            }
        }
    }
}