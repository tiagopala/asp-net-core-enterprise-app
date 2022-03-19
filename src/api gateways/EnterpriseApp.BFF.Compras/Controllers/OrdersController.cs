using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Controllers
{
    [Route("api/purchase/[controller]")]
    public class OrdersController : MainController
    {
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _cartService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public OrdersController(
            ICatalogService catalogService,
            IShoppingCartService cartService,
            ICustomerService customerService,
            IOrderService orderService)
        {
            _catalogService = catalogService;
            _cartService = cartService;
            _customerService = customerService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDTO order)
        {
            var cart = await _cartService.GetShoppingCart();
            var products = await _catalogService.GetItems(cart.Items.Select(p => p.ProductId));
            var endereco = await _customerService.GetAddress();

            if (!await ValidateCartProducts(cart, products))
                return CustomResponse();

            PopulateOrderData(cart, endereco, order);

            return CustomResponse(await _orderService.FinishOrder(order));
        }

        [HttpGet("last")]
        public async Task<IActionResult> LastOrder()
        {
            var order = await _orderService.GetLastOrder();

            if (order is null)
            {
                AddError("Order Not Found!");
                return CustomResponse();
            }

            return CustomResponse(order);
        }

        [HttpGet("list")]
        public async Task<IActionResult> OrderListByCustomerId()
        {
            var orders = await _orderService.GetOrderListByCustomerId();

            return orders == null ? NotFound() : CustomResponse(orders);
        }

        private async Task<bool> ValidateCartProducts(CartDTO cart, IEnumerable<ItemProductDTO> products)
        {
            // Implementar validação de produtos

            return true;
        }

        private static void PopulateOrderData(CartDTO cart, AddressDTO address, OrderDTO order)
        {
            order.Code = cart.Voucher?.Code;
            order.HasUsedVoucher = cart.HasUsedVoucher;
            order.TotalPrice = cart.TotalPrice;
            order.Discount = cart.Discount;
            order.OrderItems = cart.Items;
            order.Address = address;
        }

    }
}
