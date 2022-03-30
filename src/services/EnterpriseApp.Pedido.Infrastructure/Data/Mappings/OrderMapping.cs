using EnterpriseApp.Pedido.Domain.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseApp.Pedido.Infrastructure.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(order => order.Address, e =>
            {
                e.Property(pe => pe.Street)
                    .HasColumnName("Street");

                e.Property(pe => pe.Number)
                    .HasColumnName("Number");

                e.Property(pe => pe.Complement)
                    .HasColumnName("Complement");

                e.Property(pe => pe.Neighbourhood)
                    .HasColumnName("Neighbourhood");

                e.Property(pe => pe.Cep)
                    .HasColumnName("Cep");

                e.Property(pe => pe.City)
                    .HasColumnName("City");

                e.Property(pe => pe.State)
                    .HasColumnName("State");
            });

            builder.Property(c => c.Code)
                .HasDefaultValueSql("NEXT VALUE FOR OrdersSequence");

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);

            builder.ToTable("Orders");
        }
    }
}
