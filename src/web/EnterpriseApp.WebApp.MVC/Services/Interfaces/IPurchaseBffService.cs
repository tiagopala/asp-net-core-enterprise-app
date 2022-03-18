using EnterpriseApp.Core.Communication;
using EnterpriseApp.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface IPurchaseBffService
    {
        // Cart
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<int> GetQuantityFromCart();
        Task<ResponseResult> AddShoppingCartItem(ItemShoppingCartViewModel cartDTO);
        Task<ResponseResult> UpdateShoppingCartItem(Guid productId, ItemShoppingCartViewModel cartDTO);
        Task<ResponseResult> RemoveShoppingCartItem(Guid productId);
        Task<ResponseResult> ApplyVoucherShoppingCart(string voucher);

        // Order
        Task<ResponseResult> FinishOrder(OrderTransactionViewModel transactionOrder);
        Task<OrderViewModel> GetLastOrder();
        Task<IEnumerable<OrderViewModel>> GetOrderListByCustomerId();
        OrderTransactionViewModel MapToTransactionOrder(ShoppingCartViewModel cart, AddressViewModel address);
    }
}
