using EnterpriseApp.Core.Communication;
using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class CustomersService : MainService, ICustomersService
    {
        private readonly HttpClient _httpClient;

        public CustomersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseResult> AddAddress(AddressViewModel address)
        {
            var addressContent = GetContent(address);

            var response = await _httpClient.PostAsync("addresses", addressContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<AddressViewModel> GetAddress()
        {
            var response = await _httpClient.GetAsync("addresses");

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                return null;

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<AddressViewModel>(response);
        }
    }
}
