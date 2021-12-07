using EnterpriseApp.Catalogo.API.Models;
using EnterpriseApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) {}

        // DbSets
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
            => await base.SaveChangesAsync() > 0;
    }
}
