using EnterpriseApp.Core.DomainObjects;
using System;

namespace EnterpriseApp.Pedido.Domain.Pedidos
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnityPrice { get; private set; }
        public string ProductImage { get; set; }

        // EF Rel.
        public Order Order { get; set; }

        public OrderItem(Guid productId, string productName, int quantity, decimal unityPrice, string productImage = null)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnityPrice = unityPrice;
            ProductImage = productImage;
        }

        // EF ctor
        protected OrderItem() { }

        internal decimal CalculatePrice()
        {
            return Quantity * UnityPrice;
        }
    }
}
