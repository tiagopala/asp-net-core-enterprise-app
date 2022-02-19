using EnterpriseApp.Core.Communication;
using EnterpriseApp.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface IPurchaseBffService
    {
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<int> GetQuantityFromCart();
        Task<ResponseResult> AddShoppingCartItem(ItemProductViewModel cartDTO);
        Task<ResponseResult> UpdateShoppingCartItem(Guid productId, ItemProductViewModel cartDTO);
        Task<ResponseResult> RemoveShoppingCartItem(Guid productId);
    }
}
