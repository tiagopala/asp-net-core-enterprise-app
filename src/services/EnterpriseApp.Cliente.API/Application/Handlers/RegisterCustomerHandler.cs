using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Core.Extensions;
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
            if (!request.Validate())
                return request.ValidationResult;

            // Utilizar o método abaixo para erros na regra de negócio
            // Exemplo: Se cliente já existe no banco de dados
            if (true)
                request.ValidationResult.AddCustomError("CPF already taken.");

            return request.ValidationResult;
        }
    }
}
