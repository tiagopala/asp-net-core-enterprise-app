using EnterpriseApp.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(Guid id);
        void Add(Product product);
        void Update(Product product);
    }
}
