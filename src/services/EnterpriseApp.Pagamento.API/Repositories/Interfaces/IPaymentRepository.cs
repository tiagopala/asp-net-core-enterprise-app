using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Pagamento.API.Models;

namespace EnterpriseApp.Pagamento.API.Repositories.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void AddPayment(Payment payment);
    }
}
