using EnterpriseApp.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    [Obsolete("This service is obsolete. Use ICatalogServiceRefit instead.", true)]
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProduct(Guid id);
    }
}
