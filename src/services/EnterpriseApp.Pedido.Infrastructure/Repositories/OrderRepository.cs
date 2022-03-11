using EnterpriseApp.Core.Data;
using EnterpriseApp.Pedido.Domain.Pedidos;
using EnterpriseApp.Pedido.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork 
            => _dbContext;

        public DbConnection GetConnection() 
            => _dbContext.Database.GetDbConnection();

        public async Task<Order> GetById(Guid id)
            => await _dbContext.Orders.FindAsync(id);

        public async Task<OrderItem> GetItemById(Guid id)
            => await _dbContext.OrderItems.FindAsync(id);

        public async Task<IEnumerable<Order>> GetListByClientId(Guid customerId)
            => await _dbContext.Orders
                .Include(p => p.OrderItems)
                .AsNoTracking()
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();

        public void Add(Order Order)
            => _dbContext.Orders.Add(Order);

        public void Update(Order Order)
            => _dbContext.Orders.Update(Order);

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
            => await _dbContext.OrderItems
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);

        public void Dispose()
            => _dbContext.Dispose();
    }
}
