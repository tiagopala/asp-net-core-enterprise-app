using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EnterpriseApp.Carrinho.API.Models
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid ShoppingCartId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ShoppingCartCustomer ShoppingCartCustomer { get; set; } // Entity Framework Relation

        public void LinkShoppingCartCustomer(Guid shoppingCartId)
            => ShoppingCartId = shoppingCartId;

        public decimal CalculatePrice()
            => Quantity * Price;

        public void AddQuantity(int productUnities)
            => Quantity += productUnities;

        public void UpdateQuantity(int productUnities)
            => Quantity = productUnities;

        public bool IsValid()
            => new ShoppingCartItemValidator().Validate(this).IsValid;

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
                    .WithMessage(item => $"Item {item.Name} minimum quantity is 1");

                RuleFor(item => item.Quantity)
                    .LessThan(5)
                    .WithMessage(item => $"Item {item.Name} maximum quantity is 5");

                RuleFor(item => item.Price)
                    .GreaterThan(0)
                    .WithMessage(item => $"Item {item.Name} price must be greater than 0.");
            }
        }
    }
}