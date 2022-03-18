using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    public class OrdersController : MainController
    {
        private readonly ICustomersService _customersService;
        private readonly IPurchaseBffService _purchaseBffService;

        public OrdersController(
            ICustomersService customersService,
            IPurchaseBffService purchaseBffService)
        {
            _customersService = customersService;
            _purchaseBffService = purchaseBffService;
        }

        [HttpGet]
        [Route("delivery-address")]
        public async Task<IActionResult> DeliveryAddress()
        {
            var cart = await _purchaseBffService.GetShoppingCart();

            if (cart.Items.Count == 0)
                return RedirectToAction("Index", "Cart");

            var address = await _customersService.GetAddress();

            var order = _purchaseBffService.MapToTransactionOrder(cart, address);

            return View(order);
        }

        [HttpGet]
        [Route("payment")]
        public async Task<IActionResult> Payment()
        {
            var cart = await _purchaseBffService.GetShoppingCart();

            if (cart.Items.Count == 0)
                return RedirectToAction("Index", "Cart");

            var order = _purchaseBffService.MapToTransactionOrder(cart, null);

            return View(order);
        }

        [HttpPost]
        [Route("finish-order")]
        public async Task<IActionResult> FinishOrder(OrderTransactionViewModel transactionOrder)
        {
            if (!ModelState.IsValid) 
                return View("Payment", _purchaseBffService.MapToTransactionOrder(await _purchaseBffService.GetShoppingCart(), null));

            var response = await _purchaseBffService.FinishOrder(transactionOrder);

            if (ResponsePossuiErros(response))
            {
                var cart = await _purchaseBffService.GetShoppingCart();

                if (cart.Items.Count == 0) 
                    return RedirectToAction("Index", "Carrinho");

                var orderMap = _purchaseBffService.MapToTransactionOrder(cart, null);

                return View("Payment", orderMap);
            }

            return RedirectToAction("OrderFinished");
        }

        [HttpGet]
        [Route("order-finished")]
        public async Task<IActionResult> OrderFinished()
            => View("OrderConfirmation", await _purchaseBffService.GetLastOrder());

        [HttpGet("my-orders")]
        public async Task<IActionResult> MyOrders()
            => View(await _purchaseBffService.GetOrderListByCustomerId());
    }
}
