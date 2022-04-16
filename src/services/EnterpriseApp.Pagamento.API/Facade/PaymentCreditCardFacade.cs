using EnterpriseApp.Pagamento.API.AppSettings;
using EnterpriseApp.Pagamento.API.Enums;
using EnterpriseApp.Pagamento.API.Models;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using PaymentGateway = External.Payments.Gateway.Payme;

namespace EnterpriseApp.Pagamento.API.Facade
{
    public class PaymentCreditCardFacade : IPaymentFacade
    {
        private readonly PaymentConfig _paymentConfig;

        public PaymentCreditCardFacade(IOptions<PaymentConfig> options)
        {
            _paymentConfig = options.Value;
        }

        public async Task<Transaction> AuthorizePayment(Payment payment)
        {
            var paymeService = new PaymentGateway.PaymeService(_paymentConfig.DefaultApiKey, _paymentConfig.DefaultEncryptionKey);

            var cardHashGen = new PaymentGateway.CardHash(paymeService)
            {
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.CardName,
                CardExpirationDate = payment.CreditCard.MonthYearDueDate,
                CardCvv = payment.CreditCard.Cvv
            };

            string cardHash = cardHashGen.Generate();

            var transaction = new PaymentGateway.Transaction(paymeService)
            {
                CardHash = cardHash,
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.CardName,
                CardExpirationDate = payment.CreditCard.MonthYearDueDate,
                CardCvv = payment.CreditCard.Cvv,
                PaymentMethod = PaymentGateway.PaymentMethod.CreditCard,
                Amount = payment.Price
            };

            return ToTransaction(await transaction.AuthorizeCardTransaction());
        }

        public async Task<Transaction> CapturePayment(Transaction transaction)
        {
            var paymeService = new PaymentGateway.PaymeService(_paymentConfig.DefaultApiKey, _paymentConfig.DefaultEncryptionKey);

            var gatewayTransaction = ToPaymentGatewayTransaction(transaction, paymeService);

            return ToTransaction(await gatewayTransaction.CaptureCardTransaction());
        }

        public async Task<Transaction> CancelAuthorization(Transaction transaction)
        {
            var paymeService = new PaymentGateway.PaymeService(_paymentConfig.DefaultApiKey, _paymentConfig.DefaultEncryptionKey);

            var gatewayTransaction = ToPaymentGatewayTransaction(transaction, paymeService);

            return ToTransaction(await gatewayTransaction.CancelAuthorization());
        }

        private static Transaction ToTransaction(PaymentGateway.Transaction transaction)
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Status = (TransactionStatusEnum)transaction.Status,
                TotalPrice = transaction.Amount,
                CreditCardBrand = transaction.CardBrand,
                AuthorizationCode = transaction.AuthorizationCode,
                Tax = transaction.Cost,
                Date = transaction.TransactionDate,
                NSU = transaction.Nsu,
                TID = transaction.Tid
            };
        }

        public static PaymentGateway.Transaction ToPaymentGatewayTransaction(Transaction transaction, PaymentGateway.PaymeService paymeService)
        {
            return new PaymentGateway.Transaction(paymeService)
            {
                Status = (PaymentGateway.TransactionStatus)transaction.Status,
                Amount = transaction.TotalPrice,
                CardBrand = transaction.CreditCardBrand,
                AuthorizationCode = transaction.AuthorizationCode,
                Cost = transaction.Tax,
                Nsu = transaction.NSU,
                Tid = transaction.TID
            };
        }
    }
}
