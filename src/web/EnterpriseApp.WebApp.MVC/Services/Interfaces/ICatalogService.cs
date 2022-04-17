using EnterpriseApp.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    //[Obsolete("This service is obsolete. Use ICatalogServiceRefit instead.", true)]
    public interface ICatalogService
    {
        Task<PagedViewModel<ProductViewModel>> GetAllProducts(int pageSize, int pageIndex, string query = null);
        Task<ProductViewModel> GetProduct(Guid id);
    }
}
