using EnterpriseApp.Pagamento.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseApp.Pagamento.API.Data.Mappings
{
    public class PaymentsMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Ignore(p => p.CreditCard);

            // 1 : N => Payment 1 : N Transactions
            builder.HasMany(p => p.Transactions)
                .WithOne(t => t.Payment)
                .HasForeignKey(t => t.PaymentId);

            builder.ToTable("Payments");
        }
    }
}
