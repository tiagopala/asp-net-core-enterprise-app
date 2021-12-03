using EnterpriseApp.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogServiceRefit
    {
        [Get("/api/catalog/products")]
        Task<IEnumerable<ProductViewModel>> GetAllProducts();

        [Get("/api/catalog/products/{id}")]
        Task<ProductViewModel> GetProduct(Guid id);
    }
}
