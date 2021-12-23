using EnterpriseApp.Cliente.API.Application.Commands.Validations;
using EnterpriseApp.Core.Messages;
using System;

namespace EnterpriseApp.Cliente.API.Application.Commands
{
    public class RegisterCustomerCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool Validate()
        {
            ValidationResult = new RegisterCustomerCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
