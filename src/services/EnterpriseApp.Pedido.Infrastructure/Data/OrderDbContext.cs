using EnterpriseApp.Core.Data;
using EnterpriseApp.Core.Messages;
using EnterpriseApp.Pedido.Domain.Pedidos;
using EnterpriseApp.Pedido.Domain.Vouchers;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Infrastructure.Data
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Voucher> Vouchers => Set<Voucher>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);

            modelBuilder.HasSequence<int>("OrdersSequence").StartsAt(1000).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

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
