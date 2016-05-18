using System.Reflection;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;

namespace Gite.WebSite
{
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