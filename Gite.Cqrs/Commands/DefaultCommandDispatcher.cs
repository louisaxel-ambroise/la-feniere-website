using System;
using Gite.Cqrs.Extensions;
using Ninject;
using ReflectionMagic;

namespace Gite.Cqrs.Commands
{
    public class DefaultCommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel _kernel;
        private readonly Type[] _handlerTypes;

        public DefaultCommandDispatcher(IKernel kernel, Type[] handlerTypes)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            if (handlerTypes == null) throw new ArgumentNullException("handlerTypes");

            _kernel = kernel;
            _handlerTypes = handlerTypes;
        }

        public void Dispatch<T>(T command) where T : Command
        {
            var handlerType = _handlerTypes.SingleForType<T>();

            _kernel.Get(handlerType).AsDynamic().Handle(command);
        }
    }
}