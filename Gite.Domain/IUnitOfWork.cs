using System;

namespace Gite.Model
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Rollback();
        bool IsDisposed();
    }
}