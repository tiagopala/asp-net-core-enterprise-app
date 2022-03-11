using EnterpriseApp.Core.Data;
using EnterpriseApp.Pedido.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Infrastructure.Data
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        public PedidosContext(DbContextOptions<PedidosContext> options) : base(options) { }

        public DbSet<Voucher> Vouchers => Set<Voucher>();

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("CreationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreationDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreationDate").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;

            return sucesso;
        }
    }
}
