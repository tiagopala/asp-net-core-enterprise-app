using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using EnterpriseApp.Core.Communication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services
{
    public class ShoppingCartService : MainService, IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartDTO> GetShoppingCart()
        {
            var response = await _httpClient.GetAsync("shoppingcart");

            if (!response.IsSuccessStatusCode)
                HandleResponseErrors(response);

            return await DeserializeResponseMessage<CartDTO>(response);
        }

        public async Task<ResponseResult> AddShoppingCartItem(ItemCartDTO cartDTO)
        {
            var itemContent = GetContent(cartDTO);

            var response = await _httpClient.PostAsync("shoppingcart", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateShoppingCartItem(ItemCartDTO cartDTO)
        {
            var itemContent = GetContent(cartDTO);

            var response = await _httpClient.PutAsync($"shoppingcart/{cartDTO.ProductId}", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveShoppingCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"shoppingcart/{productId}");

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> ApplyVoucher(VoucherDTO voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("shoppingcart/apply-voucher", itemContent);


            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
