using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Extensions;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Application.Handlers
{
    public abstract class BaseHandler<T> where T : Entity, IAggregateRoot
    {
        private readonly IRepository<T> _repository;

        public BaseHandler(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> PersistData(ValidationResult validationResult)
        {
            var successfullCommit = await _repository.UnitOfWork.Commit();

            if (successfullCommit is false)
                validationResult.AddCustomError("The operation could not be completed. Try again later.");

            return validationResult;
        }
    }
}
