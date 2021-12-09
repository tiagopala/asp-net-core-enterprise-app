using EnterpriseApp.Cliente.API.Models;
using EnterpriseApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Data
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        public CustomerContext(DbContextOptions options) : base(options)
        {
            base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            base.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        // DbSets
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Address> Addresses => Set<Address>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
        }

        public async Task<bool> Commit()
            => await base.SaveChangesAsync() > 0;
    }
}
