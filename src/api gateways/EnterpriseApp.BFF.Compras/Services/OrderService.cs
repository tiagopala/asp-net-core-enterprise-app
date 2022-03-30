using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using EnterpriseApp.Core.Communication;
using System.Collections.Generic;
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

        public async Task<ResponseResult> FinishOrder(OrderDTO order)
        {
            var itemContent = GetContent(order);

            var response = await _httpClient.PostAsync("orders", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<OrderDTO> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("orders/last");

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                return null;

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<OrderDTO>(response);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrderListByCustomerId()
        {
            var response = await _httpClient.GetAsync("orders/list");

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                return null;

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<IEnumerable<OrderDTO>>(response);
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var response = await _httpClient.GetAsync($"vouchers/{code}");

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                return null;

            HandleResponseErrors(response);

            return await DeserializeResponseMessage<VoucherDTO>(response);
        }
    }
}
