using EnterpriseApp.Catalogo.API.Models;
using EnterpriseApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product> GetProduct(Guid id)
            => await _context.Products.FindAsync(id);

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
