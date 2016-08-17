using System;
using Gite.Cqrs.Extensions;
using Ninject;
using ReflectionMagic;

namespace Gite.Cqrs.Events
{
    public sealed class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IKernel _kernel;
        private readonly Type[] _handlerTypes;

        public DefaultEventDispatcher(IKernel kernel, Type[] handlerTypes)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            if (handlerTypes == null) throw new ArgumentNullException("handlerTypes");

            _kernel = kernel;
            _handlerTypes = handlerTypes;
        }

        public void Dispatch<T>(T @event) where T : Event
        {
            var handlers = _handlerTypes.ForType(@event.GetType());

            foreach (var eventHandlerType in handlers) _kernel.Get(eventHandlerType).AsDynamic().Handle(@event);
        }
    }
}