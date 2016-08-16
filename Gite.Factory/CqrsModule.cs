using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Cqrs.Events;
using Gite.Database.Cqrs;
using Gite.Messaging.Events;
using Gite.Model;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Gite.Factory
{
    public class CqrsModule : NinjectModule
    {
        public override void Load()
        {
            var eventTypes = typeof (ReservationCreated).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof (Event)));
            Bind<IAggregateLoader>().To<AggregateLoader>().WithConstructorArgument("eventTypes", eventTypes.ToArray());
            Bind<IEventLoader>().To<SqlEventLoader>();
            Bind<IEventStore>().To<SqlEventStore>();
            Bind<ICommandDispatcher>().To<DefaultCommandDispatcher>();
            Bind<IEventDispatcher>().To<DefaultEventDispatcher>();

            Kernel.Bind(x => x.FromAssemblyContaining<IUnitOfWork>().SelectAllClasses().InheritedFrom(typeof(IEventHandler)).BindAllInterfaces());
            Kernel.Bind(x => x.FromAssemblyContaining<IUnitOfWork>().SelectAllClasses().InheritedFrom(typeof(ICommandHandler)).BindAllInterfaces());
        }
    }
}