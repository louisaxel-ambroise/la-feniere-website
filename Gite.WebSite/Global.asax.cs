using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Gite.WebSite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            BootstrapNinject();
            MvcStartup();
        }

        private static void MvcStartup()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(typeof(ScopedControllerFactory));
        }

        private static void BootstrapNinject()
        {
            NinjectContainer.RegisterModules(NinjectModules.Modules);
        }
    }
}
