using EnterpriseApp.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetProducts(int pageSize, int pageIndex, string query = null);
        Task<Product> GetProduct(Guid id);
        Task<IEnumerable<Product>> GetProductsById(string ids);
        void Add(Product product);
        void Update(Product product);
    }
}
