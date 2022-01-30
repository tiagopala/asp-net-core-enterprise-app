using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem.Validate())
                return;

            shoppingCartItem.LinkShoppingCartCustomer(Id);

            if (HasItem(shoppingCartItem.ProductId))
            {
                var existentItem = GetItem(shoppingCartItem.ProductId);

                existentItem.UpdateQuantity(shoppingCartItem.Quantity);

                shoppingCartItem = existentItem;

                Items.Remove(existentItem);
            }

            Items.Add(shoppingCartItem);

            CalculateTotalPrice();
        }
    }
}