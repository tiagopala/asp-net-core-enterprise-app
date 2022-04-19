using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    //[Obsolete("This service is obsolete. Use ICatalogServiceRefit interface instead.", true)]
    public class CatalogService : MainService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedViewModel<ProductViewModel>> GetAllProducts(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products?pageSize={pageSize}&pageIndex={pageIndex}&query={query}");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<PagedViewModel<ProductViewModel>>(response);
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
