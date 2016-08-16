using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Extensions;
using ReflectionMagic;

namespace Gite.Cqrs.Events
{
    public sealed class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IEventStore _eventStore;
        private readonly IEventHandler[] _handlers;

        public DefaultEventDispatcher(IEventStore eventStore, IEventHandler[] handlers)
        {
            if (eventStore == null) throw new ArgumentNullException("eventStore");
            if (handlers == null) throw new ArgumentNullException("handlers");

            _eventStore = eventStore;
            _handlers = handlers;
        }

        public void Dispatch<T>(T @event) where T : Event
        {
            _eventStore.Store(@event);
            var handlers = _handlers.ForType(@event.GetType());

            foreach (var eventHandler in handlers) eventHandler.AsDynamic().Handle(@event);
        }
    }
}