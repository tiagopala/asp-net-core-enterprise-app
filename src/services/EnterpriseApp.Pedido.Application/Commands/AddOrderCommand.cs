using EnterpriseApp.Core.Messages;
using EnterpriseApp.Pedido.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace EnterpriseApp.Pedido.Application.Commands
{
    public class AddOrderCommand : Command
    {
        // Pedido
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        // Voucher
        public string VoucherCode { get; set; }
        public bool HasUsedVoucher { get; set; }
        public decimal Discount { get; set; }

        // Endereco
        public AddressDTO Address { get; set; }

        // Cartao
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }

        public override bool Validate()
        {
            ValidationResult = new AddOrderValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AddOrderValidator : AbstractValidator<AddOrderCommand>
        {
            public AddOrderValidator()
            {
                RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Inválid customer Id");

                RuleFor(c => c.OrderItems.Count)
                    .GreaterThan(0)
                    .WithMessage("Order must have at least 1 item");

                RuleFor(c => c.TotalPrice)
                    .GreaterThan(0)
                    .WithMessage("Invalid total price");

                RuleFor(c => c.CardNumber)
                    .CreditCard()
                    .WithMessage("Invalid credit card");

                RuleFor(c => c.CardName)
                    .NotNull()
                    .WithMessage("Card name must be informed");

                RuleFor(c => c.CardCvv.Length)
                    .GreaterThan(2)
                    .LessThan(5)
                    .WithMessage("CVV must contain between 2 and 5 digits");

                RuleFor(c => c.CardExpirationDate)
                    .NotNull()
                    .WithMessage("Card expiration date must be informed");
            }
        }
    }
}
