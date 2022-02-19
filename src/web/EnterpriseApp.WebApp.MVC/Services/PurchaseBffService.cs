using EnterpriseApp.Core.Communication;
using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class PurchaseBffService : MainService, IPurchaseBffService
    {
        private readonly HttpClient _httpClient;

        public PurchaseBffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region Domínio do Carrinho de Compras
        public async Task<ShoppingCartViewModel> GetShoppingCart()
        {
            var response = await _httpClient.GetAsync("shoppingcart");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<ShoppingCartViewModel>(response);
        }

        public async Task<int> GetQuantityFromCart()
        {
            var response = await _httpClient.GetAsync("shoppingcart/items/quantity");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<int>(response);
        }

        public async Task<ResponseResult> AddShoppingCartItem(ItemProductViewModel cartDTO)
        {
            var itemContent = GetContent(cartDTO);

            var response = await _httpClient.PostAsync("shoppingcart/items/", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateShoppingCartItem(Guid productId, ItemProductViewModel cartDTO)
        {
            var itemContent = GetContent(cartDTO);

            var response = await _httpClient.PutAsync($"shoppingcart/items/{productId}", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveShoppingCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"shoppingcart/items/{productId}");

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }
        #endregion
    }
}
