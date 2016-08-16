using Gite.Cqrs.Commands;

namespace Gite.Cqrs.Extensions
{
    public static class CommandHandlerExtensions
    {
        public static ICommandHandler<T> SingleForType<T>(this ICommandHandler[] handlers)
        {
            return null; // TODO: implement
        }  
    }
}