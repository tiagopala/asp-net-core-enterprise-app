using EnterpriseApp.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseApp.Catalogo.API.Data
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) {}

        // DbSets
        public DbSet<Produto> Produtos => Set<Produto>();
    }
}
