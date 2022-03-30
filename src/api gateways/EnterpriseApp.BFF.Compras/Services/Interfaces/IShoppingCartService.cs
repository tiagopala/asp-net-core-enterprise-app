using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.Core.Communication;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<CartDTO> GetShoppingCart();
        Task<ResponseResult> AddShoppingCartItem(ItemCartDTO cartDTO);
        Task<ResponseResult> UpdateShoppingCartItem(ItemCartDTO cartDTO);
        Task<ResponseResult> RemoveShoppingCartItem(Guid productId);
        Task<ResponseResult> ApplyVoucher(VoucherDTO voucher);
    }
}
