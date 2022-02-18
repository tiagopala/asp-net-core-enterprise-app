using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Controllers
{
    [Authorize]
    [Route("purchase/shoppingcart")]
    public class ShoppingCartController : MainController
    {
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(
            ICatalogService catalogService,
            IShoppingCartService cartService)
        {
            _catalogService = catalogService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetShoppingCart());
        }

        [HttpPost]
        [Route("items")]
        public async Task<IActionResult> AddShoppingCartItem(ItemCartDTO item)
        {
            var product = await _catalogService.GetById(item.ProductId);

            await ValidateCartItem(product, item.Quantity, true);
            
            if (!IsValidOperation()) 
                return CustomResponse();

            item.Name  = product.Name;
            item.Price = product.Price;
            item.Image = product.Image;

            var resposta = await _cartService.AddShoppingCartItem(item);

            return CustomResponse(resposta);
        }

        [HttpGet]
        [Route("items/quantity")]
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var quantity = await _cartService.GetShoppingCart();
            return quantity?.Items.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPut]
        [Route("items/{produtoId}")]
        public async Task<IActionResult> UpdateShoppingCartItem(Guid productId, ItemCartDTO item)
        {
            var product = await _catalogService.GetById(productId);

            await ValidateCartItem(product, item.Quantity);

            if (!IsValidOperation()) 
                return CustomResponse();

            var resposta = await _cartService.UpdateShoppingCartItem(item);

            return CustomResponse(resposta);
        }

        [HttpDelete]
        [Route("items/{produtoId}")]
        public async Task<IActionResult> RemoveItemFromShoppingCart(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product is null)
            {
                AddError("Produto inexistente!");
                return CustomResponse();
            }

            var resposta = await _cartService.RemoveShoppingCartItem(productId);

            return CustomResponse(resposta);
        }

        private async Task ValidateCartItem(ItemProductDTO product, int quantity, bool addProduct = false)
        {
            if (product is null) 
                AddError("Produto inexistente!");

            if (quantity < 1) 
                AddError($"Escolha ao menos uma unidade do produto {product.Name}");

            var cart = await _cartService.GetShoppingCart();

            var cartItem = cart.Items.FirstOrDefault(p => p.ProductId == product.Id);

            if (cartItem != null && addProduct && cartItem.Quantity + quantity > product.StockQuantity)
            {
                AddError($"O produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
                return;
            }

            if (quantity > product.StockQuantity)
                AddError($"O produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
        }
    }
}
