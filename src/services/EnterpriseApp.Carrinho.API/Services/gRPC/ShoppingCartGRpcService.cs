using EnterpriseApp.Carrinho.API.Data;
using EnterpriseApp.Carrinho.API.Models;
using EnterpriseApp.Core.Services.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.Services.gRPC
{
    [Authorize]
    public class ShoppingCartGRpcService : ShoppingCartServices.ShoppingCartServicesBase
    {
        private readonly ILogger<ShoppingCartGRpcService> _logger;
        private readonly IUserService _userService;
        private readonly ShoppingCartContext _dbContext;

        public ShoppingCartGRpcService(
            ILogger<ShoppingCartGRpcService> logger,
            IUserService userService,
            ShoppingCartContext dbContext)
        {
            _logger = logger;
            _userService = userService;
            _dbContext = dbContext;
        }

        public override async Task<ShoppingCartCustomerResponse> GetShoppingCart(GetShoppingCartRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Serviço chamado - {nameof(GetShoppingCart)} - gRPC");

            var shoppingCart = await GetShoppingCartFromDatabase();

            return ToShoppingCartCustomerResponse(shoppingCart);
        }

        private async Task<ShoppingCartCustomer> GetShoppingCartFromDatabase()
            => await _dbContext.CartCustomer.Include(x => x.Items).FirstOrDefaultAsync(x => x.CustomerId == _userService.GetUserId());

        private static ShoppingCartCustomerResponse ToShoppingCartCustomerResponse(ShoppingCartCustomer shoppingCart)
        {
            var shoppingCartProto = new ShoppingCartCustomerResponse
            {
                Id = shoppingCart.Id.ToString(),
                Customerid = shoppingCart.CustomerId.ToString(),
                Totalprice = (double)shoppingCart.TotalPrice,
                Discount = (double)shoppingCart.Discount,
                Hasusedvoucher = shoppingCart.HasUsedVoucher,
            };

            if (shoppingCart.Voucher != null)
            {
                shoppingCartProto.Voucher = new VoucherResponse
                {
                    Code = shoppingCart.Voucher.Code,
                    Percent = (double?)shoppingCart.Voucher.Percent ?? 0,
                    Discountvalue = (double?)shoppingCart.Voucher.DiscountValue?? 0,
                    Discounttype = (int)shoppingCart.Voucher.DiscountType
                };
            }

            foreach (var item in shoppingCart.Items)
            {
                shoppingCartProto.Items.Add(new ShoppingCartItemResponse
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Image = item.Image,
                    Productid = item.ProductId.ToString(),
                    Quantity = item.Quantity,
                    Price = (double)item.Price
                });
            }

            return shoppingCartProto;
        }
    }
}
