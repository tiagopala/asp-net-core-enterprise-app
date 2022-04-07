using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Pagamento.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Repositories.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void AddPayment(Payment payment);
        Task<Payment> GetPaymentByOrderId(Guid orderId);
        Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId);
    }
}
