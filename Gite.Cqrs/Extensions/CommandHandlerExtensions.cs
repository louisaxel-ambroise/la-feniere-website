using System.Linq;
using Gite.Cqrs.Commands;

namespace Gite.Cqrs.Extensions
{
    public static class CommandHandlerExtensions
    {
        public static ICommandHandler<T> SingleForType<T>(this ICommandHandler[] handlers)
        {
            return (ICommandHandler<T>) handlers.SingleOrDefault(h => h.GetType().GetInterfaces().Any(x => x.Name == typeof(ICommandHandler<>).Name && x.GenericTypeArguments[0].Name == typeof(T).Name));
        }  
    }
}