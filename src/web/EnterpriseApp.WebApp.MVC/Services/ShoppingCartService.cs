using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class ShoppingCartService : MainService, IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCart()
        {
            var response = await _httpClient.GetAsync("shoppingcart/");

            if(!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<ShoppingCartViewModel>(response);
        }

        public async Task<ResponseResult> AddShoppingCartItem(ItemProductViewModel produto)
        {
            var itemContent = GetContent(produto);

            var response = await _httpClient.PostAsync("api/shoppingcart/", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> UdateShoppingCartItem(Guid produtoId, ItemProductViewModel produto)
        {
            var itemContent = GetContent(produto);

            var response = await _httpClient.PutAsync($"shoppingcart/{produto.ProductId}", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> RemoveShoppingCartItem(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"shoppingcart/{produtoId}");

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return RetornoOk();
        }
    }
}