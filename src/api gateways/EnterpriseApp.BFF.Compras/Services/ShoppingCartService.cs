using EnterpriseApp.BFF.Compras.Services.Interfaces;
using System.Net.Http;

namespace EnterpriseApp.BFF.Compras.Services
{
    public class ShoppingCartService : MainService, IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
