using EnterpriseApp.Pagamento.API.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Facade
{
    public interface IPaymentFacade
    {
        Task<Transaction> AuthorizePayment(Payment payment);
    }
}
