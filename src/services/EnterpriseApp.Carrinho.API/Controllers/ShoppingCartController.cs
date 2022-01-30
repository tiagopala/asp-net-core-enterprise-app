using EnterpriseApp.API.Core.Controllers;
using EnterpriseApp.Carrinho.API.Data;
using EnterpriseApp.Carrinho.API.Models;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
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
            var shoppingCart = GetShoppingCartFromDatabase();

            return shoppingCart is null ? CustomResponse(shoppingCart) : CustomResponse(new ShoppingCartCustomer());
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = GetShoppingCartFromDatabase();

            if (shoppingCart is null)
                CreateNewShoppingCartWithItem(shoppingCartItem);
            else
                UpdateShoppingCartWithExistentItem(shoppingCart, shoppingCartItem);

            if (!CheckOperation()) 
                return CustomResponse();

            var result = await _context.SaveChangesAsync();

            if (result <= 0)
                AddError("Database operation could not be completed.");

            return CustomResponse();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateItemFromShoppingCart(Guid productId, ShoppingCartCustomer shoppingCart)
        {
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItemFromShoppingCart(Guid productId)
        {
            return Ok();
        }

        private ShoppingCartCustomer GetShoppingCartFromDatabase()
            => _context.CartCustomer
                .Include(x => x.Items)
                .FirstOrDefault(x => x.CustomerId == _userService.GetUserId());

        private ShoppingCartCustomer CreateNewShoppingCartWithItem(ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = GetShoppingCartFromDatabase();

            shoppingCart.AddShoppingCartItem(shoppingCartItem);

            _context.CartCustomer.Add(shoppingCart);

            return shoppingCart;
        }

        private void UpdateShoppingCartWithExistentItem(ShoppingCartCustomer shoppingCart, ShoppingCartItem shoppingCartItem)
        {
            var productExistentItem = shoppingCart.HasItem(shoppingCartItem.ProductId);

            shoppingCart.AddShoppingCartItem(shoppingCartItem);

            if (productExistentItem)
            {
                _context.CartItems.Update(shoppingCart.GetItem(shoppingCartItem.ProductId));
            }
            else
            {
                _context.CartItems.Add(shoppingCartItem);
            }

            _context.CartCustomer.Update(shoppingCart);
        }
    }
}
