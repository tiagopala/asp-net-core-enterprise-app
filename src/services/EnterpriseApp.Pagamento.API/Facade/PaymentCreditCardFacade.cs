using EnterpriseApp.Pagamento.API.AppSettings;
using EnterpriseApp.Pagamento.API.Enums;
using EnterpriseApp.Pagamento.API.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExternalGateway = External.Payments.Gateway.Payme;

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
            var paymeService = new ExternalGateway.PaymeService(_paymentConfig.DefaultApiKey, _paymentConfig.DefaultEncryptionKey);

            var cardHashGen = new ExternalGateway.CardHash(paymeService)
            {
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.CardName,
                CardExpirationDate = payment.CreditCard.MonthYearDueDate,
                CardCvv = payment.CreditCard.Cvv
            };

            string cardHash = cardHashGen.Generate();

            var transaction = new ExternalGateway.Transaction(paymeService)
            {
                CardHash = cardHash,
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.CardName,
                CardExpirationDate = payment.CreditCard.MonthYearDueDate,
                CardCvv = payment.CreditCard.Cvv,
                PaymentMethod = ExternalGateway.PaymentMethod.CreditCard,
                Amount = payment.Price
            };

            return ToTransaction(await transaction.AuthorizeCardTransaction());
        }

        public static Transaction ToTransaction(ExternalGateway.Transaction transaction)
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
    }
}
