using EnterpriseApp.BFF.Compras.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.BFF.Compras.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<AddressDTO> GetAddress();
    }
}
