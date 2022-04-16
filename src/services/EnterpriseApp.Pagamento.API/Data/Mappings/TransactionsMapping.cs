using EnterpriseApp.Pagamento.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseApp.Pagamento.API.Data.Mappings
{
    public class TransactionsMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Payment)
                .WithMany(p => p.Transactions);

            builder.ToTable("Transactions");
        }
    }
}
