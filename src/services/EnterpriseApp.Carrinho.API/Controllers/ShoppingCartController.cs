using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Carrinho.API.Data;
using EnterpriseApp.Carrinho.API.Models;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.Controllers
{
    [Authorize]
    [Route("[controller]/shopping-cart")]
    public class ShoppingCartController : MainController
    {
        private readonly IUserService _userService;
        private readonly ShoppingCartContext _context;

        public ShoppingCartController(
            IUserService userService,
            ShoppingCartContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingCart()
        {
            var userId = _userService.GetUserId();

            var shoppingCart = GetShoppingCartFromDatabase(userId);

            if (shoppingCart is null)
                return new NotFoundObjectResult(new ProblemDetails { Title = "ShoppingCartNotFound" });

            return CustomResponse(shoppingCart);
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

        private ShoppingCartCustomer GetShoppingCartFromDatabase(Guid customerId )
            => _context.CartCustomer.First(x => x.CustomerId == customerId);
    }
}
