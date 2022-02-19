using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public class CarrinhoViewComponent : ViewComponent
    {
        private readonly IPurchaseBffService _purchaseBffService;

        public CarrinhoViewComponent(IPurchaseBffService purchaseBffService)
        {
            _purchaseBffService = purchaseBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _purchaseBffService.GetShoppingCart() ?? new ShoppingCartViewModel());
        }
    }
}
