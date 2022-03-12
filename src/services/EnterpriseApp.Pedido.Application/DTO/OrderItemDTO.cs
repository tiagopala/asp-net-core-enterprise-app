using EnterpriseApp.Pedido.Domain.Pedidos;
using System;

namespace EnterpriseApp.Pedido.Application.DTO
{
    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }

    public static class OrderItemExtensions
    {
        public static OrderItem ToOrderItem(this OrderItemDTO orderItemDTO)
            => new(orderItemDTO.ProductId, orderItemDTO.Name, orderItemDTO.Quantity, orderItemDTO.Price, orderItemDTO.Image);
    }
}
