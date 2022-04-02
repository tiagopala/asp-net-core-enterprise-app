using EnterpriseApp.Core.Data;
using EnterpriseApp.Pagamento.API.Data;
using EnterpriseApp.Pagamento.API.Models;
using EnterpriseApp.Pagamento.API.Repositories.Interfaces;
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

        public void AddPayment(Payment payment)
            => _paymentsContext.Payments.Add(payment);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
