using EnterpriseApp.WebApp.MVC.Configuration;
using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class CatalogService : MainService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(
            HttpClient httpClient,
            IOptions<CatalogApiConfig> catalogAPIConfig)
        {
            httpClient.BaseAddress = new Uri(catalogAPIConfig.Value.Endpoint);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("/api/catalog/products");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetProduct(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<ProductViewModel>(response);
        }
    }
}
