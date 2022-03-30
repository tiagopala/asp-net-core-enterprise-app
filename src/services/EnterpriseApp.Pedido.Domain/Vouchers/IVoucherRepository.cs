using EnterpriseApp.Core.DomainObjects;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Domain.Vouchers
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucherByCode(string code);
        void Update(Voucher voucher);
    }
}
