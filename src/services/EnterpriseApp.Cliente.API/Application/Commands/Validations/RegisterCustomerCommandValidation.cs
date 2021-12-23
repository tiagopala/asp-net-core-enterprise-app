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
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Must(ValidateEmail).WithMessage("{PropertyName} is not in correct format. example@example.com");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Must(ValidateCpf).WithMessage("{PropertyName} is not in correct format. 000.000.000-00");
        }

        protected static bool ValidateCpf(string cpf)
            => Core.DomainObjects.Cpf.Validate(cpf);

        protected static bool ValidateEmail(string email)
            => Core.DomainObjects.Email.Validate(email);
    }
}
