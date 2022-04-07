using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.Pagamento.API.Enums;
using EnterpriseApp.Pagamento.API.Facade;
using EnterpriseApp.Pagamento.API.Models;
using EnterpriseApp.Pagamento.API.Repositories.Interfaces;
using EnterpriseApp.Pagamento.API.Services.Interfaces;
using FluentValidation.Results;
using System;
using System.Linq;
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

        public async Task<ResponseMessage> CapturePayment(Guid orderId)
        {
            var transactions = await _paymentRepository.GetTransactionsByOrderId(orderId);
            var authorizedTransaction = transactions?.FirstOrDefault(t => t.Status == TransactionStatusEnum.Authorized);
            var validationResult = new ValidationResult();

            if (authorizedTransaction is null) throw new DomainException($"Transaction not found for this OrderId:{orderId}.");

            var paymenTransaction = await _paymentFacade.CapturePayment(authorizedTransaction);

            if (paymenTransaction.Status != TransactionStatusEnum.Paid)
            {
                validationResult.Errors.Add(new ValidationFailure("Payment",$"Error while trying to capture payment for this OrderId:{orderId}"));

                return new ResponseMessage(validationResult);
            }

            paymenTransaction.PaymentId = authorizedTransaction.PaymentId;
            _paymentRepository.AddTransaction(paymenTransaction);

            if (!await _paymentRepository.UnitOfWork.Commit())
            {
                validationResult.Errors.Add(new ValidationFailure("Payment", "Operation could not be completed. Try again later."));

                return new ResponseMessage(validationResult);
            }

            return new ResponseMessage(validationResult);
        }

        public async Task<ResponseMessage> CancelPayment(Guid orderId)
        {
            var transactions = await _paymentRepository.GetTransactionsByOrderId(orderId);
            var authorizedTransaction = transactions?.FirstOrDefault(t => t.Status == TransactionStatusEnum.Authorized);
            var validationResult = new ValidationResult();

            if (authorizedTransaction is null) throw new DomainException($"Transaction not found for this OrderId:{orderId}.");

            var transaction = await _paymentFacade.CancelAuthorization(authorizedTransaction);

            if (transaction.Status != TransactionStatusEnum.Cancelled)
            {
                validationResult.Errors.Add(new ValidationFailure("Payment", $"Error while trying to cancel payment for this OrderId:{orderId}"));

                return new ResponseMessage(validationResult);
            }

            transaction.PaymentId = authorizedTransaction.PaymentId;
            _paymentRepository.AddTransaction(transaction);

            if (!await _paymentRepository.UnitOfWork.Commit())
            {
                validationResult.Errors.Add(new ValidationFailure("Payment", "Operation could not be completed. Try again later."));

                return new ResponseMessage(validationResult);
            }

            return new ResponseMessage(validationResult);
        }
    }
}
