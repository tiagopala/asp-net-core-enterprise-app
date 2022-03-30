using EnterpriseApp.Catalogo.API.Models;
using EnterpriseApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
            => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetProducts()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product> GetProduct(Guid id)
            => await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetProductsById(string ids)
        {
            string[] idList = ids.Split(",");

            IEnumerable<Guid> guidIds = idList.Select(x => Guid.Parse(x));

            return await _context.Products.AsNoTracking().Where(x => guidIds.Contains(x.Id) && x.Active).ToListAsync();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
