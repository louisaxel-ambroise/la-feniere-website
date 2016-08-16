using System;
using Gite.Cqrs.Extensions;
using Gite.Cqrs.Persistance;
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

        public void Dispatch<T>(T @event) where T : IEvent
        {
            var handlers = _handlers.ForType<T>();

            foreach (var eventHandler in handlers) eventHandler.AsDynamic().Handle(@event);

            _eventStore.Store(@event);
        }
    }
}