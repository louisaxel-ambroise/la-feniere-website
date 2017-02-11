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
                var mailAddress = ConfigurationManager.AppSettings["Mail.Configuration.Address"];
                var password = ConfigurationManager.AppSettings["Mail.Configuration.Password"];
                var baseUrl = ConfigurationManager.AppSettings["Website.BaseUrl"];
                var connectionString = ConfigurationManager.ConnectionStrings["GiteDatabase"].ConnectionString;

                return new NinjectModule[] {
                    new DomainModule(mailAddress, password, baseUrl),
                    new DatabaseModule(connectionString)
                };
            }
        }
    }
}