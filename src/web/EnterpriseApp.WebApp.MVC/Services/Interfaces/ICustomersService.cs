using EnterpriseApp.Core.Communication;
using EnterpriseApp.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<AddressViewModel> GetAddress();
        Task<ResponseResult> AddAddress(AddressViewModel address);
    }
}
