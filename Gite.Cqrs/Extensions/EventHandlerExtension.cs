using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Extensions
{
    public static class EventHandlerExtension
    {
        public static IEnumerable<IEventHandler> ForType<T>(this IEventHandler[] handlers) where T : IEvent
        {
            return handlers.Where(h => h.GetType().GetInterfaces().Any(x => x.Name == typeof(IEventHandler<>).Name && x.GenericTypeArguments[0].Name == typeof(T).Name)).ToList();
        } 
    }
}