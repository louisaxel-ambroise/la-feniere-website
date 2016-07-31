using System.Web.Http;
using Gite.WebSite;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Gite.WebSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = CreateKernel();
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);

            app
                .UseNinjectMiddleware(() => kernel)
                //.UseBasicAuthentication(new AuthenticationOptions{ Password = "admin", Username = "admin" })
                .UseNinjectWebApi(configuration);
        }

        protected virtual IKernel CreateKernel()
        {
            return new StandardKernel(NinjectModules.Modules);
        }
    }
}