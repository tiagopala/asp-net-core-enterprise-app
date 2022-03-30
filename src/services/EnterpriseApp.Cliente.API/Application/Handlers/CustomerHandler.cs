using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Application.Events;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Cliente.API.Business.Models;
using EnterpriseApp.Core.Extensions;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Application.Handlers
{
    public class CustomerHandler : BaseHandler<Customer>,
        IRequestHandler<RegisterCustomerCommand, ValidationResult>,
        IRequestHandler<AddAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerHandler(ICustomerRepository customerRepository, IMediatorHandler mediatorHandler) : base(customerRepository, mediatorHandler)
            => _customerRepository = customerRepository;

        public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validate())
                return request.ValidationResult;

            var customer = new Customer(request.Id, request.Name, request.Email, request.Cpf);

            var customerFound = await _customerRepository.GetByCpf(request.Cpf);

            if (customerFound is not null)
            {
                request.ValidationResult.AddCustomError($"CPF: {customerFound.Cpf.Number} already taken.");
                return request.ValidationResult;
            }

            _customerRepository.Add(customer);
            var successfullOperation = await PersistData();

            if(successfullOperation is false)
            {
                request.ValidationResult.AddCustomError("The operation could not be completed. Try again later.");
                return request.ValidationResult;
            }

            await MediatorHandler.PublishEvent(new CustomerRegisteredEvent(request.Id, request.Name, request.Email, request.Cpf));

            return request.ValidationResult;
        }

        public async Task<ValidationResult> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validate())
                return request.ValidationResult;

            var address = new Address(request.Street, request.Number, request.Complement, request.Neighbourhood, request.Cep, request.City, request.State, request.CustomerId);
            
            _customerRepository.AddAddress(address);

            var successfullOperation = await PersistData();

            if (successfullOperation is false)
                request.ValidationResult.AddCustomError("The operation could not be completed. Try again later.");

            return request.ValidationResult;
        }
    }
}
