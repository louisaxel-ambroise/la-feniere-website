using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Extensions
{
    public static class EventHandlerExtension
    {
        public static IEnumerable<IEventHandler> ForType(this IEventHandler[] handlers, Type eventType)
        {
            return handlers.Where(h => h.GetType().GetInterfaces().Any(x =>
            {
                var matches = x.Name == typeof (IEventHandler<>).Name &&
                              x.GenericTypeArguments[0].Name == eventType.Name;
                return matches;
            })).ToList();
        } 
    }
}