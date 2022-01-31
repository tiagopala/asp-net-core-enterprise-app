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
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICatalogServiceRefit _catalogService;

        public CartController(
            IShoppingCartService carrinhoService,
            ICatalogServiceRefit catalogoService)
        {
            _shoppingCartService = carrinhoService;
            _catalogService = catalogoService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _shoppingCartService.GetShoppingCart());
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemProductViewModel itemProduto)
        {
            var produto = await _catalogService.GetProduct(itemProduto.ProductId);

            ValidarItemCarrinho(produto, itemProduto.Quantity);
            if (!OperacaoValida()) return View("Index", await _shoppingCartService.GetShoppingCart());

            itemProduto.Name = produto.Name;
            itemProduto.Price = produto.Price;
            itemProduto.Image = produto.Image;

            var resposta = await _shoppingCartService.AddShoppingCartItem(itemProduto);

            if (ResponsePossuiErros(resposta)) return View("Index", await _shoppingCartService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var produto = await _catalogService.GetProduct(produtoId);

            ValidarItemCarrinho(produto, quantidade);
            if (!OperacaoValida()) return View("Index", await _shoppingCartService.GetShoppingCart());

            var itemProduto = new ItemProductViewModel { ProductId = produtoId, Quantity = quantidade };
            var resposta = await _shoppingCartService.UdateShoppingCartItem(produtoId, itemProduto);

            if (ResponsePossuiErros(resposta)) return View("Index", await _shoppingCartService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogService.GetProduct(produtoId);

            if (produto == null)
            {
                AdicionarErroValidacao("Produto inexistente!");
                return View("Index", await _shoppingCartService.GetShoppingCart());
            }

            var resposta = await _shoppingCartService.RemoveShoppingCartItem(produtoId);

            if (ResponsePossuiErros(resposta)) return View("Index", await _shoppingCartService.GetShoppingCart());

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