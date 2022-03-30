using EnterpriseApp.Core.Data;
using EnterpriseApp.Pedido.Domain.Vouchers;
using EnterpriseApp.Pedido.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrderDbContext _dbContext;

        public VoucherRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task<Voucher> GetVoucherByCode(string code)
            => await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.Code == code);

        public void Update(Voucher voucher)
            => _dbContext.Vouchers.Update(voucher);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
