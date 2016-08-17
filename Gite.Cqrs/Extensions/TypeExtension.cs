using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Commands;
using Gite.Cqrs.Events;

namespace Gite.Cqrs.Extensions
{
    public static class TypeExtension
    {
        public static IEnumerable<Type> ForType(this Type[] handlers, Type eventType)
        {
            return handlers.Where(h => h.GetInterfaces().Any(x =>
            {
                var matches = x.Name == typeof (IEventHandler<>).Name &&
                              x.GenericTypeArguments[0].Name == eventType.Name;
                return matches;
            })).ToList();
        }

        public static Type SingleForType<T>(this Type[] handlers)
        {
            return handlers.SingleOrDefault(h => h.GetInterfaces().Any(x => x.Name == typeof(ICommandHandler<>).Name && x.GenericTypeArguments[0].Name == typeof(T).Name));
        }  
    }
}