using EnterpriseApp.BFF.Compras.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services.Interfaces
{
    public interface IOrderService
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
}
