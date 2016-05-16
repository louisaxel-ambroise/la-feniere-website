using Ninject;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using System.Collections.Generic;
using Ninject.Modules;
using System.Reflection;
using Gite.WebSite.App_Start;

namespace Gite.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NinjectContainer.RegisterModules(NinjectModules.Modules);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class NinjectResolver : IDependencyResolver
    {
        public IKernel Kernel { get; private set; }
        public NinjectResolver(params NinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public NinjectResolver(Assembly assembly)
        {
            Kernel = new StandardKernel();
            Kernel.Load(assembly);
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }

    public class NinjectModules
    {
        public static NinjectModule[] Modules
        {
            get
            {
                return new[] { new DatabaseModule() };
            }
        }
    }

    public class NinjectContainer
    {
        private static NinjectResolver _resolver;

        public static void RegisterModules(NinjectModule[] modules)
        {
            _resolver = new NinjectResolver(modules);
            DependencyResolver.SetResolver(_resolver);
        }

        public static void RegisterAssembly()
        {
            _resolver = new NinjectResolver(Assembly.GetExecutingAssembly());

            DependencyResolver.SetResolver(_resolver);
        }

        public static T Resolve<T>()
        {
            return _resolver.Kernel.Get<T>();
        }
    }
}
