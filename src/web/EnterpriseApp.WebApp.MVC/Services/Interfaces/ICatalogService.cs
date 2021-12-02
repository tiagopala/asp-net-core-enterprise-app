using EnterpriseApp.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProduct(Guid id);
    }
}
