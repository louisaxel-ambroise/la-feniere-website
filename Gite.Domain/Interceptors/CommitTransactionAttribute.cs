using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

namespace Gite.Model.Interceptors
{
    public class CommitTransactionAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            var unitOfWork = request.Kernel.Get<IUnitOfWork>();

            return new CommitTransactionInterceptor(unitOfWork);
        }
    }
}