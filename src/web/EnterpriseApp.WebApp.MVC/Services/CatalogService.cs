using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    [Obsolete("This service is obsolete. Use ICatalogServiceRefit interface instead.", true)]
    public class CatalogService : MainService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IHttpClientFactory clientFactory)
        {
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
