using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Mediator;
using System.Threading.Tasks;

namespace EnterpriseApp.Core.Messages
{
    public abstract class BaseHandler<T> where T : Entity, IAggregateRoot
    {
        private readonly IRepository<T> _repository;
        protected readonly IMediatorHandler MediatorHandler;

        public BaseHandler(
            IRepository<T> repository,
            IMediatorHandler mediatorHandler)
        {
            _repository = repository;
            MediatorHandler = mediatorHandler;
        }

        public async Task<bool> PersistData()
        {
            var successfullCommit = await _repository.UnitOfWork.Commit();

            return successfullCommit;
        }
    }
}
