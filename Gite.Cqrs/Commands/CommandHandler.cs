using System.Collections.Generic;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Commands
{
    public abstract class CommandHandler<T> : ICommandHandler<T>
    {
        private readonly IList<Event> _events;

        protected CommandHandler()
        {
            _events = new List<Event>();
        }

        protected void RaiseEvent<T>(T @event) where T : Event
        {
            _events.Add(@event);
        }

        public IEnumerable<Event> Events
        {
            get { return _events; }
        }

        public abstract void Handle(T command);
    }
}