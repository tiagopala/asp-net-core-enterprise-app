using EnterpriseApp.BFF.Compras.Models;
using EnterpriseApp.Core.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services.Interfaces
{
    public interface IOrderService
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
        Task<ResponseResult> FinishOrder(OrderDTO order);
        Task<OrderDTO> GetLastOrder();
        Task<IEnumerable<OrderDTO>> GetOrderListByCustomerId();
    }
}
