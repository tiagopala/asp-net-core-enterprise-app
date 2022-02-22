using EnterpriseApp.Core.Data;
using EnterpriseApp.Pedido.Domain.Vouchers;
using EnterpriseApp.Pedido.Infrastructure.Data;

namespace EnterpriseApp.Pedido.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidosContext _dbContext;

        public VoucherRepository(PedidosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
