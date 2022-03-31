using EnterpriseApp.Core.Data;
using EnterpriseApp.Core.Messages;
using EnterpriseApp.Pagamento.API.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Data
{
    public class PaymentsContext : DbContext, IUnitOfWork
    {
        public PaymentsContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            // Procurando por mappings via assembly e aplicando configurações do tipo IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentsContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
            => await SaveChangesAsync() > 0;
    }
}
