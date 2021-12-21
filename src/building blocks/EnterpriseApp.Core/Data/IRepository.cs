using EnterpriseApp.Core.Data;
using System;

namespace EnterpriseApp.Core.DomainObjects
{
    public interface IRepository<T> : IDisposable where T : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
