using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.Pagamento.API.Enums;
using EnterpriseApp.Pagamento.API.Facade;
using EnterpriseApp.Pagamento.API.Models;
using EnterpriseApp.Pagamento.API.Repositories.Interfaces;
using EnterpriseApp.Pagamento.API.Services.Interfaces;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentFacade _paymentFacade;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentFacade paymentFacade, IPaymentRepository paymentRepository)
        {
            _paymentFacade = paymentFacade;
            _paymentRepository = paymentRepository;
        }

        public async Task<ResponseMessage> AuthorizePayment(Payment payment)
        {
            var validationResults = new ValidationResult();

            var transaction = await _paymentFacade.AuthorizePayment(payment);

            if (transaction.Status is not TransactionStatusEnum.Authorized)
            {
                validationResults.Errors.Add(new ValidationFailure("Payment", "Unauthorized transaction."));

                return new ResponseMessage(validationResults);
            }

            _paymentRepository.AddPayment(payment);
            var saveOperation = await _paymentRepository.UnitOfWork.Commit();

            if (saveOperation is false)
            {
                validationResults.Errors.Add(new ValidationFailure("Payment", "Operation could not be completed. Try again later."));

                // Se não for possível realizar a persistência na base de dados, devemos realizar o estorno
                // TODO: Estorno com o gateway de pagamentos
                //_paymentFacade.ChargebackPayment(transaction.NSU);

                return new ResponseMessage(validationResults);
            }

            return new ResponseMessage(validationResults);
        }
    }
}
