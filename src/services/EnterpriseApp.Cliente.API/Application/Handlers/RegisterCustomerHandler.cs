using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Core.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Application.Handlers
{
    public class RegisterCustomerHandler : CommandHandlerBase, IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            ValidationResult = request.ValidationResult;

            if (!request.Validate())
                return request.ValidationResult;

            // Utilizar o método abaixo para erros na regra de negócio
            // Exemplo: Se cliente já existe no banco de dados
            if (true)
            {
                AddCustomError("This CPF is already taken.");
                return ValidationResult;
            }

            return ValidationResult;
        }
    }
}
