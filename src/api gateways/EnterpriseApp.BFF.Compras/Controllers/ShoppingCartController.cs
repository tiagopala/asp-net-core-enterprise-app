using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.gRPC;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Controllers
{
    [Authorize]
    [Route("api/purchase/shoppingcart")]
    public class ShoppingCartController : MainController
    {
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _cartService;
        private readonly IShoppingCartGRpcService _gRpcCartService;
        private readonly IOrderService _orderService;

        public ShoppingCartController(
            ICatalogService catalogService,
            IShoppingCartService cartService,
            IShoppingCartGRpcService gRpcCartService,
            IOrderService orderService)
        {
            _catalogService = catalogService;
            _cartService = cartService;
            _gRpcCartService = gRpcCartService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => CustomResponse(await _gRpcCartService.GetShoppingCart());

        [HttpGet]
        [Route("items/quantity")]
        public async Task<int> GetQuantityFromCart()
        {
            var quantity = await _gRpcCartService.GetShoppingCart();
            return quantity?.Items.Sum(i => i.Quantity) ?? 0;
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

        [HttpPut]
        [Route("items/{productId}")]
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
        [Route("items/{productId}")]
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

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
        {
            var voucher = await _orderService.GetVoucherByCode(voucherCodigo);
            if (voucher is null)
            {
                AddError("Voucher informed is either invalid or not found.");
                return CustomResponse();
            }

            var response = await _cartService.ApplyVoucher(voucher);

            return CustomResponse(response);
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
