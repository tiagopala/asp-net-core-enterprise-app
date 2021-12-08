using EnterpriseApp.Cliente.API.Models;
using EnterpriseApp.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseApp.Cliente.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Name)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.OwnsOne(c => c.Cpf, customer =>
            {
                customer.Property(cpf => cpf.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, customer =>
            {
                customer.Property(email => email.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });

            // Relacionamento 1 : 1 => Cliente : Endereço
            builder.HasOne(customer => customer.Address)
                .WithOne(address => address.Customer);

            builder.ToTable("Customers");
        }
    }
}
