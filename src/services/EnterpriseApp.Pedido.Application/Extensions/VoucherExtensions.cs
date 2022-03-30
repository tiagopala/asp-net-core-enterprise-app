using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Domain.Vouchers;

namespace EnterpriseApp.Pedido.Application.Extensions
{
    public static class VoucherExtensions
    {
        public static VoucherDTO ToVoucherDTO(this Voucher voucher)
            => new()
            {
                Code = voucher.Code,
                Percent = voucher.Percent,
                DiscountValue = voucher.DiscountValue,
                DiscountType = (int)voucher.DiscountType
            };
    }
}
