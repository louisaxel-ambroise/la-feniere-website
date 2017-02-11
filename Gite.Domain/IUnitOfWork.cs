using System;

namespace Gite.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Rollback();
        bool IsDisposed();
    }
}