using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Carrinho.API.Models;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.Controllers
{
    [Authorize]
    [Route("[controller]/shopping-cart")]
    public class ShoppingCartController : MainController
    {
        private readonly IUserService _userService;

        public ShoppingCartController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ShoppingCartCustomer> GetShoppingCart()
        {
            var userId = _userService.GetUserId();

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart()
        {
            return Ok();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateItemFromShoppingCart()
        {
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItemFromShoppingCart()
        {
            return Ok();
        }
    }
}
