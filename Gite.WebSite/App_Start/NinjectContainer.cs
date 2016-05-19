using System.Reflection;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;

namespace Gite.WebSite
{
    public class NinjectContainer
    {
        public static NinjectResolver Resolver;

        public static void RegisterModules(NinjectModule[] modules)
        {
            Resolver = new NinjectResolver(modules);
            DependencyResolver.SetResolver(Resolver);
        }

        public static void RegisterAssembly()
        {
            Resolver = new NinjectResolver(Assembly.GetExecutingAssembly());

            DependencyResolver.SetResolver(Resolver);
        }

        public static T Resolve<T>()
        {
            return Resolver.Kernel.Get<T>();
        }
    }
}