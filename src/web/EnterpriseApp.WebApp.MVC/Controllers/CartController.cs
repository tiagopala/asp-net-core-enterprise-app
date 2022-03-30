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
        public async Task<IActionResult> AddItemAtCart(ItemShoppingCartViewModel itemProduct)
        {
            var resposta = await _purchaseBffService.AddShoppingCartItem(itemProduct);

            if (ResponsePossuiErros(resposta)) return View("Index", await _purchaseBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateCartItem(Guid produtoId, int quantidade)
        {
            var item = new ItemShoppingCartViewModel { ProductId = produtoId, Quantity = quantidade };
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

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCodigo)
        {
            var resposta = await _purchaseBffService.ApplyVoucherShoppingCart(voucherCodigo);

            if (ResponsePossuiErros(resposta)) return View("Index", await _purchaseBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }
    }
}