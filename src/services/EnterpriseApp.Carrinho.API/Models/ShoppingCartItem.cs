using FluentValidation;
using System;

namespace EnterpriseApp.Carrinho.API.Models
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
            Id = Guid.NewGuid();
            Validator = new();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid ShoppingCartId { get; set; }

        public ShoppingCartItemValidator Validator { get; }

        public ShoppingCartCustomer ShoppingCartCustomer { get; set; } // Entity Framework Relation

        public void LinkShoppingCartCustomer(Guid shoppingCartId)
            => ShoppingCartId = shoppingCartId;

        public decimal CalculatePrice()
            => Quantity * Price;

        public void UpdateQuantity(int productUnities)
            => Quantity += productUnities;

        public bool Validate()
            => Validator.Validate(this).IsValid;

        public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItem>
        {
            public ShoppingCartItemValidator()
            {
                RuleFor(item => item.ProductId)
                    .NotEmpty()
                    .NotEqual(Guid.Empty)
                    .WithMessage("Invalid Product Id");

                RuleFor(item => item.Name)
                    .NotEmpty()
                    .WithMessage("Invalid Product Name");

                RuleFor(item => item.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Item minimum quantity is 1");

                RuleFor(item => item.Quantity)
                    .LessThan(15)
                    .WithMessage("Item maximum quantity is 15");

                RuleFor(item => item.Price)
                    .GreaterThan(0)
                    .WithMessage("Item Price must be greater than 0.");
            }
        }
    }
}