using EnterpriseApp.BFF.Compras.AppSettings;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using Microsoft.Extensions.Options;
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
