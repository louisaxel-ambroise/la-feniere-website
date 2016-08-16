using System.Collections.Generic;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Commands
{
    public abstract class CommandHandler<T> : ICommandHandler<T>
    {
        private readonly IList<IEvent> _events;

        protected CommandHandler()
        {
            _events = new List<IEvent>();
        }

        protected void RaiseEvent<T>(T @event) where T : IEvent
        {
            _events.Add(@event);
        }

        public IEnumerable<IEvent> Events
        {
            get { return _events; }
        }

        public abstract void Handle(T command);
    }
}