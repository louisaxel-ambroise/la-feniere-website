using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using Gite.WebSite.Models.Admin;
using System.Configuration;

namespace Gite.WebSite
{
    public class NinjectResolver : IDependencyResolver
    {
        public IKernel Kernel { get; private set; }

        public NinjectResolver(params NinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);

            // Admin Credentials
            var adminUsername = ConfigurationManager.AppSettings["Admin.Username"];
            var adminPassword = ConfigurationManager.AppSettings["Admin.Password"];
            Kernel.Bind<Credentials>().ToConstant(new Credentials { Username = adminUsername, Password = adminPassword });
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
}