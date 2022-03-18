using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services
{
    public class CustomerService : MainService, ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddressDTO> GetAddress()
        {
            var response = await _httpClient.GetAsync("addresses");

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                return null;

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<AddressDTO>(response);
        }
    }
}
