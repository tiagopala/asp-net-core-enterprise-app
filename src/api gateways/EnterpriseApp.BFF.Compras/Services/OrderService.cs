using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services
{
    public class OrderService : MainService, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var response = await _httpClient.GetAsync(code);

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                return null;

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<VoucherDTO>(response);
        }
    }
}
