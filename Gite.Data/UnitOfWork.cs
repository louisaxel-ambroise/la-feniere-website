using System;
using Gite.Domain;
using NHibernate;

namespace Gite.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISession _session;
        private ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
            _transaction = _session.BeginTransaction();
        }

        public void Dispose()
        {
            if (!IsDisposed())
            {
                Rollback();
            }

            _transaction = null;
            _session = null;
        }

        public void SaveChanges()
        {
            EnsureIsActive();

            _session.Flush();
            _transaction.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            EnsureIsActive();

            _transaction.Rollback();
            _transaction = null;
        }

        public bool IsDisposed()
        {
            return _transaction == null || !_transaction.IsActive;
        }

        private void EnsureIsActive()
        {
            if (IsDisposed())
            {
                throw new ObjectDisposedException("UnitOfWork was already committed or rollbacked.");
            }
        }
    }
}