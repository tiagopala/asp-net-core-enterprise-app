using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services
{
    public class CatalogService : MainService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ItemProductDTO> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"products/{id}");

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<ItemProductDTO>(response);
        }

        public async Task<IEnumerable<ItemProductDTO>> GetItems(IEnumerable<Guid> ids)
        {
            var idsRequest = string.Join(",", ids);

            var response = await _httpClient.GetAsync($"products/list/{idsRequest}/");

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<IEnumerable<ItemProductDTO>>(response);
        }
    }
}
