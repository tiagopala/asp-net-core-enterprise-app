using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using static EnterpriseApp.Carrinho.API.Models.ShoppingCartItem;

namespace EnterpriseApp.Carrinho.API.Models
{
    public class ShoppingCartCustomer
    {
        public ShoppingCartCustomer() { } // Entity Framework Constructor
        public ShoppingCartCustomer(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }
        
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public Guid CustomerId { get; set; }

        public void CalculateTotalPrice()
            => TotalPrice = Items.Sum(item => item.CalculatePrice());

        public bool HasItem(Guid productId)
            => Items.Any(p => p.ProductId == productId);

        public ShoppingCartItem GetItem(Guid productId)
            => Items.FirstOrDefault(p => p.ProductId == productId);

        public (bool isValid, ValidationResult validationResult) Validate()
        {
            var errors = Items.SelectMany(i => new ShoppingCartItemValidator().Validate(i).Errors).ToList();

            errors.AddRange(new ShoppingCartCustomerValidator().Validate(this).Errors);

            var validationResult = new ValidationResult(errors);

            return (validationResult.IsValid, validationResult);
        }

        public void AddShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            shoppingCartItem.LinkShoppingCartCustomer(Id);

            if (HasItem(shoppingCartItem.ProductId))
            {
                var existentItem = GetItem(shoppingCartItem.ProductId);

                existentItem.AddQuantity(shoppingCartItem.Quantity);

                shoppingCartItem = existentItem;

                Items.Remove(existentItem);
            }

            Items.Add(shoppingCartItem);

            CalculateTotalPrice();
        }

        private void UpdateShoppingCartItem(ShoppingCartItem item)
        {
            if (!item.IsValid())
                return;

            item.LinkShoppingCartCustomer(item.Id);

            var existentItem = GetItem(item.ProductId);

            Items.Remove(item);
            Items.Add(existentItem);

            CalculateTotalPrice();
        }

        public void UpdateQuantity(ShoppingCartItem item, int quantity)
        {
            item.UpdateQuantity(quantity);

            UpdateShoppingCartItem(item);
        }

        public void RemoveShoppingCartItem(ShoppingCartItem item)
        {
            Items.Remove(item);

            CalculateTotalPrice();
        }

        public class ShoppingCartCustomerValidator : AbstractValidator<ShoppingCartCustomer>
        {
            public ShoppingCartCustomerValidator()
            {
                RuleFor(cart => cart.CustomerId)
                    .NotEmpty()
                    .NotEqual(Guid.Empty)
                    .WithMessage("Unknown Customer Id");

                RuleFor(cart => cart.Items.Count)
                    .GreaterThan(0)
                    .WithMessage("Car does not have any item");

                RuleFor(cart => cart.TotalPrice)
                    .GreaterThan(0)
                    .WithMessage("Cart total price must be greather than 0");
            }
        }
    }
}