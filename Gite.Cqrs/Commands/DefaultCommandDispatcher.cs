using System;
using Gite.Cqrs.Events;
using Gite.Cqrs.Extensions;

namespace Gite.Cqrs.Commands
{
    public class DefaultCommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandler[] _commandHandlers;
        private readonly IEventDispatcher _eventDispatcher;

        public DefaultCommandDispatcher(IEventDispatcher eventDispatcher, ICommandHandler[] commandHandlers)
        {
            if (commandHandlers == null) throw new ArgumentNullException("commandHandlers");
            if (eventDispatcher == null) throw new ArgumentNullException("eventDispatcher");

            _commandHandlers = commandHandlers;
            _eventDispatcher = eventDispatcher;
        }

        public void Dispatch<T>(T command) where T : Command
        {
            var commandHandler = _commandHandlers.SingleForType<T>();
            commandHandler.Handle(command);

            foreach (var @event in commandHandler.Events)
            {
                _eventDispatcher.Dispatch(@event);
            }
        }
    }
}