using Dapper;
using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Domain.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            connectionString.Query<OrderDTO>(sql);

            return MapOrder();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrderListByCustomerId(Guid customerId)
        {
            var orders = await _orderRepository.GetListByCustomerId(customerId);

            return orders.Select(x => x.ToOrderDTO());
        }

        private static OrderDTO MapOrder()
        {
            return new OrderDTO();
        }
    }

    public interface IOrderQueries
    {
        Task<OrderDTO> GetLastOrder(Guid customerId);
        Task<IEnumerable<OrderDTO>> GetOrderListByCustomerId(Guid customerId);
    }
}
