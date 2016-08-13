using System;
using Gite.Database;
using Gite.Database.Repositories;
using Gite.Model;
using Gite.Model.Repositories;
using NHibernate;
using Ninject.Extensions.UnitOfWork;
using Ninject.Modules;

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
            
            Bind<IReservationRepository>().To<ReservationRepository>();
            Bind<IReservationWeekRepository>().To<ReservationWeekRepository>();
            Bind<ISession>().ToMethod(ctx => SessionProvider.OpenSession()).InUnitOfWorkScope();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
