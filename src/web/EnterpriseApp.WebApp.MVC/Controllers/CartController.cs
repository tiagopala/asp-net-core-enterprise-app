using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    [Authorize]
    [Route("shopping-cart")]
    public class CartController : MainController
    {
        private readonly IPurchaseBffService _purchaseBffService;

        public CartController(IPurchaseBffService purchaseBffService)
        {
            _purchaseBffService = purchaseBffService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _purchaseBffService.GetShoppingCart());
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddItemAtCart(ItemProductViewModel itemProduct)
        {
            var resposta = await _purchaseBffService.AddShoppingCartItem(itemProduct);

            if (ResponsePossuiErros(resposta)) return View("Index", await _purchaseBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid produtoId, int quantidade)
        {
            var item = new ItemProductViewModel { ProductId = produtoId, Quantity = quantidade };
            var resposta = await _purchaseBffService.UpdateShoppingCartItem(produtoId, item);

            if (ResponsePossuiErros(resposta)) return View("Index", await _purchaseBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItemFromCart(Guid produtoId)
        {
            var resposta = await _purchaseBffService.RemoveShoppingCartItem(produtoId);

            if (ResponsePossuiErros(resposta)) return View("Index", await _purchaseBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        private void ValidarItemCarrinho(ProductViewModel produto, int quantidade)
        {
            if (produto == null) AdicionarErroValidacao("Produto inexistente!");
            if (quantidade < 1) AdicionarErroValidacao($"Escolha ao menos uma unidade do produto {produto.Name}");
            if (quantidade > produto.StockQuantity) AdicionarErroValidacao($"O produto {produto.Name} possui {produto.StockQuantity} unidades em estoque, você selecionou {quantidade}");
        }
    }
}