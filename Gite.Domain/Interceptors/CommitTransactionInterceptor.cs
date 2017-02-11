using Ninject.Extensions.Interception;

namespace Gite.Domain.Interceptors
{
    public class CommitTransactionInterceptor : IInterceptor
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommitTransactionInterceptor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            _unitOfWork.SaveChanges();
        }
    }
}