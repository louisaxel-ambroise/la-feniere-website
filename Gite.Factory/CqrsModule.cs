using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Cqrs.Events;
using Gite.Database.Cqrs;
using Gite.Messaging.Events;
using Gite.Model;
using Ninject.Extensions.UnitOfWork;
using Ninject.Modules;

namespace Gite.Factory
{
    public class CqrsModule : NinjectModule
    {
        public override void Load()
        {
            var eventTypes = typeof (ReservationCreated).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof (Event)));
            var eventHandlerTypes = typeof(IUnitOfWork).Assembly.GetTypes().Where(x => typeof(IEventHandler).IsAssignableFrom(x)).ToArray();
            var commandHandlerTypes = typeof(IUnitOfWork).Assembly.GetTypes().Where(x => typeof(ICommandHandler).IsAssignableFrom(x)).ToArray();

            Bind(typeof(IAggregateManager<>)).To(typeof(AggregateManager<>)).InUnitOfWorkScope().WithConstructorArgument("eventTypes", eventTypes.ToArray());
            Bind<IEventStore>().To<SqlEventStore>().InUnitOfWorkScope();
            Bind<ICommandDispatcher>().To<DefaultCommandDispatcher>().WithConstructorArgument("handlerTypes", commandHandlerTypes);
            Bind<IEventDispatcher>().To<DefaultEventDispatcher>().WithConstructorArgument("handlerTypes", eventHandlerTypes);
        }
    }
}