using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.Pagamento.API.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ResponseMessage> AuthorizePayment(Payment payment);
    }
}
