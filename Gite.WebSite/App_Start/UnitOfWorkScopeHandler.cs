using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ninject.Extensions.UnitOfWork;

namespace Gite.WebSite
{
    public class UnitOfWorkScopeHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (UnitOfWorkScope.Create())
            {
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}