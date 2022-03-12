using EnterpriseApp.Pedido.Domain.Pedidos;
using System;
using System.Collections.Generic;

namespace EnterpriseApp.Pedido.Application.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public bool HasUsedVoucher { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public AddressDTO Address { get; set; }
    }

    public static class OrderExtensions
    {
        public static OrderDTO ToOrderDTO(this Order order)
        {
            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                Code = order.Code,
                Status = (int)order.OrderStatus,
                Date = order.CreationDate,
                TotalPrice = order.TotalPrice,
                Discount = order.Discount,
                HasUsedVoucher = order.HasUsedVoucher,
                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO()
            };

            foreach (var item in order.OrderItems)
            {
                orderDTO.OrderItems.Add(new OrderItemDTO
                {
                    Name = item.ProductName,
                    Image = item.ProductImage,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    Price = item.UnityPrice,
                    OrderId = item.OrderId
                });
            }

            orderDTO.Address = new AddressDTO
            {
                Street = order.Address.Street,
                Number = order.Address.Number,
                Complement = order.Address.Complement,
                Neighbourhood = order.Address.Neighbourhood,
                Cep = order.Address.Cep,
                City = order.Address.City,
                State = order.Address.State,
            };

            return orderDTO;
        }
    }
}
