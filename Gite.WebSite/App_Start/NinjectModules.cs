using System.Configuration;
using Gite.Factory;
using Ninject.Modules;

namespace Gite.WebSite
{
    public class NinjectModules
    {
        public static NinjectModule[] Modules
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["GiteDatabase"].ConnectionString;

                return new NinjectModule[] { new DomainModule(), new DatabaseModule(connectionString) };
            }
        }
    }
}