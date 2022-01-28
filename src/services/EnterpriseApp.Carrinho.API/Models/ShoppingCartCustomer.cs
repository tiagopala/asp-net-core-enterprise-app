using System;
using System.Collections.Generic;

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
    }
}