using System.Collections.Generic;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in T> : ICommandHandler
    {
        IEnumerable<Event> Events { get; }
        void Handle(T command);
    }
}