using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Gite.Database;
using Gite.Database.Mappings;
using Gite.Model.Business;
using NHibernate;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Configuration;

namespace Gite.WebSite.App_Start
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IReservationRepository>().To<ReservationRepository>();

            Bind<ISession>().ToMethod(ctx => SessionProvider.OpenSession()).InRequestScope();
        }
    }

    public class SessionProvider
    {
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession()
        {
            if(_sessionFactory == null)
            {
                _sessionFactory = CreateSesionFactory();
            }
            return _sessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSesionFactory()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["GiteDatabase"].ConnectionString;

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ReservationMap>())
                .BuildSessionFactory();
        }
    }
}