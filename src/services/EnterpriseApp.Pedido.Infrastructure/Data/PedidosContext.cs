using EnterpriseApp.Core.Data;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages;
using EnterpriseApp.Pedido.Domain.Vouchers;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Infrastructure.Data
{
    public class PedidosContext : DbContext, IUnitOfWork
    {

        private readonly IMediatorHandler _mediatorHandler;

        public PedidosContext(DbContextOptions<PedidosContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Voucher> Vouchers => Set<Voucher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;

            return sucesso;
        }
    }
}
