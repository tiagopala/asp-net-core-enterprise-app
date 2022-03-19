using EnterpriseApp.Core.Communication;
using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class PurchaseBffService : MainService, IPurchaseBffService
    {
        private readonly HttpClient _httpClient;

        public PurchaseBffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region Domínio do Carrinho de Compras
        public async Task<ShoppingCartViewModel> GetShoppingCart()
        {
            var response = await _httpClient.GetAsync("shoppingcart");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<ShoppingCartViewModel>(response);
        }

        public async Task<int> GetQuantityFromCart()
        {
            var response = await _httpClient.GetAsync("shoppingcart/items/quantity");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<int>(response);
        }

        public async Task<ResponseResult> AddShoppingCartItem(ItemShoppingCartViewModel cartDTO)
        {
            var itemContent = GetContent(cartDTO);

            var response = await _httpClient.PostAsync("shoppingcart/items/", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateShoppingCartItem(Guid productId, ItemShoppingCartViewModel cartDTO)
        {
            var itemContent = GetContent(cartDTO);

            var response = await _httpClient.PutAsync($"shoppingcart/items/{productId}", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveShoppingCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"shoppingcart/items/{productId}");

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> ApplyVoucherShoppingCart(string voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("shoppingcart/apply-voucher", itemContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        #endregion

        #region Domínio de Pedidos (Orders)
        public async Task<ResponseResult> FinishOrder(OrderTransactionViewModel transactionOrder)
        {
            var orderContent = GetContent(transactionOrder);

            var response = await _httpClient.PostAsync("orders", orderContent);

            if (!response.IsSuccessStatusCode)
                return await DeserializeResponseMessage<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<OrderViewModel> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("orders/last");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<OrderViewModel>(response);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrderListByCustomerId()
        {
            var response = await _httpClient.GetAsync("orders/list");

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<IEnumerable<OrderViewModel>>(response);
        }

        public OrderTransactionViewModel MapToTransactionOrder(ShoppingCartViewModel cart, AddressViewModel address)
        {
            var order = new OrderTransactionViewModel
            {
                TotalPrice = cart.TotalPrice,
                Items = cart.Items,
                Discount = cart.Discount,
                HasUsedVoucher = cart.HasUsedVoucher,
                VoucherCode = cart.Voucher?.Code
            };

            if (address != null)
            {
                order.Address = new AddressViewModel
                {
                    Street = address.Street,
                    Number = address.Number,
                    Neighbourhood = address.Neighbourhood,
                    Cep = address.Cep,
                    Complement = address.Complement,
                    City = address.City,
                    State = address.State
                };
            }

            return order;
        }
        #endregion
    }
}
