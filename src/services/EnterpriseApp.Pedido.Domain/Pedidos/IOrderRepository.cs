using EnterpriseApp.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Domain.Pedidos
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetListByClientId(Guid clienteId);
        void Add(Order pedido);
        void Update(Order pedido);

        DbConnection GetConnection();

        /* Pedido Item */
        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid pedidoId, Guid produtoId);
    }
}
