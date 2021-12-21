using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Cliente.API.Business.Models;
using EnterpriseApp.Core.Extensions;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Application.Handlers
{
    public class RegisterCustomerHandler : BaseHandler<Customer>,IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public RegisterCustomerHandler(ICustomerRepository customerRepository) : base(customerRepository)
            => _customerRepository = customerRepository;

        public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validate())
                return request.ValidationResult;

            var customer = new Customer(request.Id, request.Name, request.Email, request.Cpf);

            var customerFound = await _customerRepository.GetByCpf(request.Cpf);

            if (customerFound is not null)
            {
                request.ValidationResult.AddCustomError($"CPF:{customerFound.Cpf} already taken.");
                return request.ValidationResult;
            }

            _customerRepository.Add(customer);

            return await PersistData(request.ValidationResult);
        }
    }
}
