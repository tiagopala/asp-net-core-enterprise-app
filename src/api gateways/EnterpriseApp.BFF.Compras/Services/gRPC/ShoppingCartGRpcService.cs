using EnterpriseApp.BFF.Compras.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services.gRPC
{
    public interface IShoppingCartGRpcService
    {
        Task<CartDTO> GetShoppingCart();
    }

    public class ShoppingCartGRpcService : IShoppingCartGRpcService
    {
        private readonly ShoppingCartServices.ShoppingCartServicesClient _gRpcShoppingCartServices;

        public ShoppingCartGRpcService(ShoppingCartServices.ShoppingCartServicesClient GRpcShoppingCartServices)
        {
            _gRpcShoppingCartServices = GRpcShoppingCartServices;
        }

        public async Task<CartDTO> GetShoppingCart()
        {
            var shoppingCart = await _gRpcShoppingCartServices.GetShoppingCartAsync(new GetShoppingCartRequest());

            return ResponseToCartDTO(shoppingCart);
        }

        private static CartDTO ResponseToCartDTO(ShoppingCartCustomerResponse response)
        {
            var cartDTO = new CartDTO
            {
                TotalPrice = Convert.ToDecimal(response.Totalprice),
                Voucher = new VoucherDTO
                {
                    Code = response?.Voucher?.Code ?? string.Empty,
                    Percent = Convert.ToDecimal(response?.Voucher?.Percent ?? 0),
                    DiscountValue = Convert.ToDecimal(response?.Voucher?.Discountvalue ?? 0),
                    DiscountType = response?.Voucher?.Discounttype ?? 0
                },
                HasUsedVoucher = response.Hasusedvoucher,
                Discount = Convert.ToDecimal(response.Discount),
                Items = new List<ItemCartDTO>()
            };

            foreach (var item in response.Items)
            {
                cartDTO.Items.Add(new ItemCartDTO
                {
                    ProductId = Guid.Parse(item.Id),
                    Name = item.Name,
                    Price = Convert.ToDecimal(item.Price),
                    Image = item.Image,
                    Quantity = item.Quantity
                });
            }

            return cartDTO;
        }
    }
}
