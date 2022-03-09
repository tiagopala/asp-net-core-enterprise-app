using EnterpriseApp.Carrinho.API.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseApp.Carrinho.API.Data
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<ShoppingCartItem> CartItems => Set<ShoppingCartItem>();
        public DbSet<ShoppingCartCustomer> CartCustomer => Set<ShoppingCartCustomer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<ShoppingCartCustomer>()
                .HasIndex(c => c.CustomerId)
                .HasDatabaseName("IDX_Customer");

            modelBuilder.Entity<ShoppingCartCustomer>()
                .Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
                {
                    v.Property(vc => vc.Code)
                   .HasColumnName("VoucherCode")
                      .HasColumnType("varchar(50)");

                    v.Property(vc => vc.DiscountType)
                        .HasColumnName("DiscountType");

                    v.Property(vc => vc.Percent)
                        .HasColumnName("Percent");

                    v.Property(vc => vc.DiscountValue)
                        .HasColumnName("DiscountValue");
                });

            modelBuilder.Entity<ShoppingCartCustomer>()
                .HasMany(c => c.Items)
                .WithOne(i => i.ShoppingCartCustomer)
                .HasForeignKey(i => i.ShoppingCartId);
        }
    }
}