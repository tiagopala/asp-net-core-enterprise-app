using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Application.Extensions;
using EnterpriseApp.Pedido.Domain.Vouchers;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCode(code);

            if (voucher is null)
                return null;

            if (!voucher.IsValidForUse())
                return null;

            return voucher.ToVoucherDTO();
        }
    }

    public interface IVoucherQueries
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
}
