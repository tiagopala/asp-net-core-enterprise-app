using EnterpriseApp.BFF.Compras.Services.Interfaces;
using System.Net.Http;

namespace EnterpriseApp.BFF.Compras.Services
{
    public class CatalogService : MainService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
