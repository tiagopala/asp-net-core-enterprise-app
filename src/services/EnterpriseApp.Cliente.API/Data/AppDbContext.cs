using EnterpriseApp.Cliente.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseApp.Cliente.API.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options){ }

        // DbSets
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
        }
    }
}
