using FluentValidation;

namespace EnterpriseApp.Cliente.API.Application.Commands.Validations
{
    public class RegisterCustomerCommandValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
