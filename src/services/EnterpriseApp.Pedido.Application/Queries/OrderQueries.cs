using Dapper;
using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Domain.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> GetLastOrder(Guid customerId)
        {
            const string sql = @"SELECT
                                    O.ID AS 'ProductId', 
                                    O.CODE, 
                                    O.HASUSEDVOUCHER, 
                                    O.DISCOUNT, 
                                    O.TOTALPRICE,
                                    O.ORDERSTATUS,
                                    O.STREET,
                                    O.NUMBER,
                                    O.NEIGHBOURHOOD,
                                    O.CEP,
                                    O.COMPLEMENT,
                                    O.CITY,
                                    O.STATE,
                                    OI.ID AS 'ProductOrderId',
                                    OI.PRODUCTNAME,
                                    OI.QUANTITY,
                                    OI.PRODUCTIMAGE,
                                    OI.UNITYPRICE
                                FROM 
                                    ORDERS O
                                INNER JOIN 
                                    ORDERITEMS OI
                                ON O.ID = OI.ORDERID
                                WHERE O.CUSTOMERID = @customerId
                                AND O.CREATIONDATE between DATEADD(minute, -3,  GETDATE()) and DATEADD(minute, 0,  GETDATE())
                                AND O.ORDERSTATUS = 1
                                ORDER BY O.CREATIONDATE DESC";

            var connectionString = _orderRepository.GetConnection();

            var ordersQueryResult = await connectionString.QueryAsync<dynamic>(sql, new { customerId });

            return MapOrder(ordersQueryResult);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrderListByCustomerId(Guid customerId)
        {
            var orders = await _orderRepository.GetListByCustomerId(customerId);

            return orders.Select(x => x.ToOrderDTO());
        }

        private static OrderDTO MapOrder(dynamic result)
        {
            int integerCode = result[0].CODE;

            var order = new OrderDTO
            {
                Code = integerCode.ToString(),
                Status = result[0].ORDERSTATUS,
                TotalPrice = result[0].TOTALPRICE,
                Discount = result[0].DISCOUNT,
                HasUsedVoucher = result[0].HASUSEDVOUCHER,

                OrderItems = new List<OrderItemDTO>(),
                
                Address = new AddressDTO
                {
                    Street = result[0].STREET,
                    Neighbourhood = result[0].NEIGHBOURHOOD,
                    Cep = result[0].CEP,
                    City = result[0].CITY,
                    Complement = result[0].COMPLEMENT,
                    State = result[0].STATE,
                    Number = result[0].NUMBER
                }
            };

            foreach (var item in result)
            {
                var orderItem = new OrderItemDTO
                {
                    Name = item.PRODUCTNAME,
                    Price = item.UNITYPRICE,
                    Quantity = item.QUANTITY,
                    Image = item.PRODUCTIMAGE
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }

        public async Task<OrderDTO> GetAuthorizedOrders()
        {
            const string sql = @"SELECT 
                                    O.Id as 'OrderId',
                                    O.Id,
                                    O.CustomerId,
                                    OI.Id as 'OrderItemId',
                                    OI.Id,
                                    OI.ProductId,
                                    OI.Quantity
                                FROM 
                                    ORDERS O
                                INNER JOIN 
                                    ORDERITEMS OI
                                ON 
                                    O.Id = OI.OrderId
                                WHERE
                                    O.OrderStatus = 1
                                ORDER BY O.CreationDate";

            var lookup = new Dictionary<Guid, OrderDTO>();
            
            var connectionString = _orderRepository.GetConnection();

            var orders = await connectionString.QueryAsync<OrderDTO, OrderItemDTO, OrderDTO>(sql, (o, oi) =>
            {
                if (!lookup.TryGetValue(o.Id, out var orderDTO))
                    lookup.Add(o.Id, orderDTO = o);

                orderDTO.OrderItems ??= new List<OrderItemDTO>();
                orderDTO.OrderItems.Add(oi);

                return o;
            }, splitOn: "ORDERID, ORDERITEMID");

            return lookup.Values.OrderBy(o => o.Date).FirstOrDefault();
        }
    }

    public interface IOrderQueries
    {
        Task<OrderDTO> GetLastOrder(Guid customerId);
        Task<IEnumerable<OrderDTO>> GetOrderListByCustomerId(Guid customerId);
        Task<OrderDTO> GetAuthorizedOrders();
    }
}
