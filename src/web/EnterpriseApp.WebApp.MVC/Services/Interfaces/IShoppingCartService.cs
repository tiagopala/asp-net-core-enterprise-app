using EnterpriseApp.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<ResponseResult> AddShoppingCartItem(ItemProductViewModel produto);
        Task<ResponseResult> UdateShoppingCartItem(Guid produtoId, ItemProductViewModel produto);
        Task<ResponseResult> RemoveShoppingCartItem(Guid produtoId);
    }
}
