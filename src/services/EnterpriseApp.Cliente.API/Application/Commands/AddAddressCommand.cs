using EnterpriseApp.Core.Messages;
using FluentValidation;
using System;

namespace EnterpriseApp.Cliente.API.Application.Commands
{
    public class AddAddressCommand : Command
    {
        public Guid CustomerId { get; set; }
        public string Street { get; }
        public string Number { get; }
        public string Complement { get; }
        public string Neighbourhood { get; }
        public string Cep { get; }
        public string City { get; }
        public string State { get; }

        public AddAddressCommand() {}

        public AddAddressCommand(Guid customerId, string street, string number, string complement, string neighbourhood, string cep, string city, string state)
        {
            CustomerId = customerId;
            Street = street;
            Number = number;
            Complement = complement;
            Neighbourhood = neighbourhood;
            Cep = cep;
            City = city;
            State = state;
        }

        public override bool Validate()
            => new AddressValidation().Validate(this).IsValid;

        public class AddressValidation : AbstractValidator<AddAddressCommand>
        {
            public AddressValidation()
            {
                RuleFor(c => c.Street)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required");

                RuleFor(c => c.Number)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required");

                RuleFor(c => c.Cep)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required");

                RuleFor(c => c.Neighbourhood)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required");

                RuleFor(c => c.City)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required");

                RuleFor(c => c.State)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required");
            }
        }
    }
}
