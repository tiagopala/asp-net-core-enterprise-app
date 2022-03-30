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
    [Route("api/[controller]")]
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
            var shoppingCart = await GetShoppingCartFromDatabase();

            if (shoppingCart is null)
                return CustomResponse(new ShoppingCartCustomer());

            return CustomResponse(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = await GetShoppingCartFromDatabase();

            if (shoppingCart is null)
                CreateNewShoppingCartWithItem(shoppingCartItem);
            else
                UpdateShoppingCartWithExistentItem(shoppingCart, shoppingCartItem);

            if (!IsValidOperation()) 
                return CustomResponse();

            await PersistData();

            return CustomResponse();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateItemFromShoppingCart(Guid productId, ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = await GetShoppingCartFromDatabase();

            var item = await GetValidatedShoppingCartItem(productId, shoppingCart, shoppingCartItem);

            if (item is null)
                return CustomResponse();

            shoppingCart.UpdateQuantity(item, shoppingCartItem.Quantity);

            ValidateShoppingCart(shoppingCart);

            if (!IsValidOperation())
                return CustomResponse();

            shoppingCart.Items.ToList().ForEach(x => x.ShoppingCartCustomer = null);

            _context.CartItems.Update(item);
            _context.CartCustomer.Update(shoppingCart);

            await PersistData();

            return CustomResponse();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItemFromShoppingCart(Guid productId)
        {
            var cart = await GetShoppingCartFromDatabase();

            var item = await GetValidatedShoppingCartItem(productId, cart);

            if (item is null)
                return CustomResponse();

            ValidateShoppingCart(cart);

            if (!IsValidOperation())
                return CustomResponse();

            cart.RemoveShoppingCartItem(item);

            _context.CartItems.Remove(item);
            _context.CartCustomer.Update(cart);

            await PersistData();

            return CustomResponse();
        }

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> AplicarVoucher(Voucher voucher)
        {
            var cart = await GetShoppingCartFromDatabase();

            cart.ApplyVoucher(voucher);

            _context.CartCustomer.Update(cart);

            await PersistData();

            return CustomResponse();
        }

        private async Task<ShoppingCartCustomer> GetShoppingCartFromDatabase()
            => await _context.CartCustomer
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.CustomerId == _userService.GetUserId());

        private void CreateNewShoppingCartWithItem(ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = new ShoppingCartCustomer(_userService.GetUserId());

            shoppingCart.AddShoppingCartItem(shoppingCartItem);

            ValidateShoppingCart(shoppingCart);

            _context.CartCustomer.Add(shoppingCart);

        }

        private void UpdateShoppingCartWithExistentItem(ShoppingCartCustomer shoppingCart, ShoppingCartItem shoppingCartItem)
        {
            var productExistentItem = shoppingCart.HasItem(shoppingCartItem.ProductId);

            shoppingCart.AddShoppingCartItem(shoppingCartItem);

            ValidateShoppingCart(shoppingCart);


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

        private async Task<ShoppingCartItem> GetValidatedShoppingCartItem(Guid productId, ShoppingCartCustomer shoppingCart, ShoppingCartItem item = null)
        {
            if (item is not null && productId != item.ProductId)
            {
                AddError("Distinct productId's.");
                return null;
            }

            if (shoppingCart is null)
            {
                AddError("ShoppingCart not found.");
                return null;
            }

            var itemFound = await _context.CartItems.FirstOrDefaultAsync(item => item.Id == item.Id && item.ProductId == productId);

            if (itemFound is null || !shoppingCart.HasItem(itemFound.ProductId))
            {
                AddError("Item not found at shopping cart.");
                return null;
            }

            return itemFound;
        }

        private async Task PersistData()
        {
            var result = await _context.SaveChangesAsync();

            if (result <= 0)
                AddError("Database operation could not be completed.");
        }

        private bool ValidateShoppingCart(ShoppingCartCustomer cart)
        {
            var (isValid, validationResult) = cart.Validate();

            if (isValid)
                return true;

            validationResult.Errors.ForEach(validationFaillure => AddError(validationFaillure.ErrorMessage));
            return false;
        }
    }
}
