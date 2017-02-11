using System;
using Gite.Database;
using Gite.Database.Repositories;
using Gite.Domain;
using Gite.Domain.Readers;
using NHibernate;
using Ninject.Extensions.UnitOfWork;
using Ninject.Modules;
using Gite.Domain.Readers;

namespace Gite.Factory
{
    public class DatabaseModule : NinjectModule
    {
        private readonly string _connectionString;

        public DatabaseModule(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException("connectionString");

            _connectionString = connectionString;
        }

        public override void Load()
        {
            SessionProvider.Register(_connectionString);
            
            Bind<ISession>().ToMethod(ctx => SessionProvider.OpenSession()).InUnitOfWorkScope();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IReservationRepository>().To<ReservationRepository>();
            Bind<ICalendarRepository>().To<CalendarRepository>();
        }
    }
}
