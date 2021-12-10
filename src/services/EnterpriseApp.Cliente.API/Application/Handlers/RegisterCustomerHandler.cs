using EnterpriseApp.Cliente.API.Application.Commands;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Application.Handlers
{
    public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            
            return request.ValidationResult;
        }
    }
}
