using EnterpriseApp.Core.Data;
using EnterpriseApp.Pagamento.API.Data;
using EnterpriseApp.Pagamento.API.Models;
using EnterpriseApp.Pagamento.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentsContext _paymentsContext;

        public PaymentRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
        }

        public IUnitOfWork UnitOfWork => _paymentsContext;

        public void AddTransaction(Transaction transaction)
            => _paymentsContext.Transactions.Add(transaction);

        public void AddPayment(Payment payment)
            => _paymentsContext.Payments.Add(payment);

        public async Task<Payment> GetPaymentByOrderId(Guid orderId)
            => await _paymentsContext.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.OrderId == orderId);

        public async Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId)
            => await _paymentsContext.Transactions.AsNoTracking().Where(t => t.PaymentId == orderId).ToListAsync();

        public void Dispose()
            => _paymentsContext.Dispose();
    }
}
