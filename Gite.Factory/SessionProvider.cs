using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Gite.Database.Mappings;
using NHibernate;

namespace Gite.Factory
{
    public class SessionProvider
    {
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                throw new Exception("Please ensure that SessionProvider.Register method is called before application starts.");
            }

            return _sessionFactory.OpenSession();
        }

        public static void Register(string connectionString)
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ReservationMap>())
                .BuildSessionFactory();
        }
    }
}