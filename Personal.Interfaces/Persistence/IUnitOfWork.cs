using System.Data.Entity;
using Personal.DomainModel;

namespace Personal.Persistence
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        DbContext CurrentContext { get; }

        void Commit();

        IRepository<T> GetRepository<T>() where T : class, IDomainObject;
    }
}