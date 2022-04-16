using Dapper;
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

        public async Task<PagedResult<Product>> GetProducts(int pageSize, int pageIndex, string query = null)
        {
            var sql = $@" 
                SELECT * 
                FROM Products
                WHERE (@Name is NULL OR Name LIKE '%' + @Name + '%')
                ORDER BY [Name]
                OFFSET {pageSize * (pageIndex - 1)} ROWS
                FETCH NEXT {pageSize} ROWS ONLY
                SELECT COUNT(Id) 
                FROM Products
                WHERE (@Name is NULL OR Name LIKE '%' + @Name + '%')";

            var gridResult = await _context.Database.GetDbConnection().QueryMultipleAsync(sql, new { Name = query });

            var products = gridResult.Read<Product>();
            var count = gridResult.Read<int>().FirstOrDefault();

            return new PagedResult<Product>
            {
                List = products,
                TotalResults = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

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
