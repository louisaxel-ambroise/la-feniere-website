using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Extensions.UnitOfWork;

namespace Gite.WebSite
{
    public class ScopedControllerFactory : DefaultControllerFactory
    {
        private UnitOfWorkScope _scope;

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            _scope = UnitOfWorkScope.Create();

            return base.CreateController(requestContext, controllerName);
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        
            _scope.Dispose();
        }
    }
}